macSoundFile

	^ '#include "sq.h"
#include <Sound.h>
#include <SoundInput.h>

/******
  Mac Sound Output Notes:

	The Squeak sound code produces 16-bit, stereo sound buffers. The was
	arrived at after experimentation on a PPC 601 at 110 MHz on which I
	found that:
	  a. using 16-bit sound only slightly increased the background CPU burden and
	  b. 16-bit sound yielded vastly superior sound quality.

	My understanding is that SoundManager 3.0 or later supports the 16-bit
	sound interface an all Macs, even if the hardware only supports 8-bits.
	If this is not true, however, change BYTES_PER_SAMPLE to 1. Then, either
	the Squeak code will need to be changed to use 8-bit sound buffers,
	or (preferrably) snd_PlaySamplesFromAtLength will need to do the conversion
	from 16 to 8 bits. I plan to cross that bridge if and when we need to.
	The code as currently written was to support Squeak code that generated
	8-bit sound buffers.

	In earlier versions, I experimented with other sound buffer formats. Here
	are all the sound buffer formats that were used at one point or another:
		1. mono,    8-bits -- packed array of bytes (not currently used)
		2. stereo,  8-bits -- as above, with L and R channels in alternate bytes (not currently used)
		3. stereo, 16-bits -- array of 32-bit words; with L and R channels in high and low half-words

	Note:  8-bit samples are encoded with 0x80 as the center (zero) value
	Note: 16-bit samples are encoded as standard, signed integers (i.e., 2''s-complement)
	Note: When the sound drive is operating in "mono", the two stereo channels are mixed
	      together. This feature was added in January, 1998.

	-- John Maloney, July 28, 1996
	-- edited: John Maloney, January 5, 1998

  Mac Sound Input Notes:

	Squeak sound input is currently defined to provide a single (mono) stream
	of signed 16-bit samples for all platforms. Platforms that only support
	8-bit sound input should convert samples to signed 16 bit values, leaving
	the low order bits zero. Since the available sampling rates differ from
	platform to platform, the client may not get the requested sampling rate;
	however, the call snd_GetRecordingSampleRate returns the sampling rate.
	On many platforms, simultaneous record and playback is permitted only if
	the input and output sampling rates are the same.

	-- John Maloney, Aug 22, 1997

******/

#define BYTES_PER_SAMPLE 2

/*** double-buffer state record ***/

typedef struct {
	int open;
	int stereo;
	int frameCount;
	int sampleRate;
	int lastFlipTime;
	int playSemaIndex;
	int bufSizeInBytes;
	int bufState0;
	int bufState1;
	int done;
} PlayStateRec;

/*** possible buffer states ***/

#define BUF_EMPTY	0
#define BUF_FULL	1
#define BUF_PLAYING	2

/*** record buffer state record ***/

/* Note: RECORD_BUFFER_SIZE should be a multiple of 4096 bytes to avoid clicking.
   (The clicking was observed on a Mac 8100; the behavior of other Macs could differ.)
*/
#define RECORD_BUFFER_SIZE (4096 * 1)
typedef struct {
	SPB paramBlock;
	int stereo;
	int bytesPerSample;
	int recordSemaIndex;
	int readIndex;  /* index of the next sample to read */
	char samples[RECORD_BUFFER_SIZE];
} RecordBufferRec, *RecordBuffer;

/*** sound output variables ***/

SndChannelPtr chan;
PlayStateRec bufState = {false, false, 0, 0, NULL, NULL, true, 0};
SndDoubleBufferHeader dblBufHeader;

/*** sound input variables ***/

RecordBufferRec recordBuffer1, recordBuffer2;
int recordingInProgress;
long soundInputRefNum;

/*** local functions ***/

pascal void DoubleBack(SndChannelPtr chan, SndDoubleBufferPtr buf);
int FillBufferWithSilence(SndDoubleBufferPtr buf);
pascal void FlipRecordBuffers(SPBPtr pb);
int MixInSamples(int count, char *srcBufPtr, int srcStartIndex, char *dstBufPtr, int dstStartIndex);

pascal void DoubleBack(SndChannelPtr chan, SndDoubleBufferPtr buf) {
	PlayStateRec *state;

	chan;  /* reference argument to avoid compiler warnings */

	/* insert a click to help user detect failure to fill buffer in time */
	*(unsigned int *) &buf->dbSoundData[0] = 0;
	*(unsigned int *) &buf->dbSoundData[4] = 0xFFFFFFFF;

	state = (PlayStateRec *) buf->dbUserInfo[0];
	if (buf->dbUserInfo[1] == 0) {
		state->bufState0 = BUF_EMPTY;
		state->bufState1 = BUF_PLAYING;
	} else {
		state->bufState0 = BUF_PLAYING;
		state->bufState1 = BUF_EMPTY;
	}

	buf->dbNumFrames = state->frameCount;
	buf->dbFlags = buf->dbFlags | dbBufferReady;
	if (state->done) {
		FillBufferWithSilence(buf);
		buf->dbFlags = buf->dbFlags | dbLastBuffer;
	} else {
		signalSemaphoreWithIndex(state->playSemaIndex);
	}
	state->lastFlipTime = ioMicroMSecs();
}

int FillBufferWithSilence(SndDoubleBufferPtr buf) {
	unsigned int *sample, *lastSample;

	sample		= (unsigned int *) &buf->dbSoundData[0];
	lastSample	= (unsigned int *) &buf->dbSoundData[bufState.bufSizeInBytes];

	/* word-fill buffer with silence */
	if (BYTES_PER_SAMPLE == 1) {
		while (sample < lastSample) {
			*sample++ = 0x80808080;  /* Note: 0x80 is zero value for 8-bit samples */
		}
	} else {
		while (sample < lastSample) {
			*sample++ = 0;
		}
	}
}

pascal void FlipRecordBuffers(SPBPtr pb) {
	/* called at interrupt time to exchange the active and inactive record buffers */
	RecordBuffer thisBuffer = (RecordBuffer) pb;
	RecordBuffer nextBuffer = (RecordBuffer) pb->userLong;

	if (pb->error == 0) {
		/* restart recording using the other buffer */
		SPBRecord(&nextBuffer->paramBlock, true);

		/* reset the read pointer for the buffer that has just been filled */
		thisBuffer->readIndex = 0;
		signalSemaphoreWithIndex(nextBuffer->recordSemaIndex);
	}
}

/*** exported sound output functions ***/

int snd_AvailableSpace(void) {
	if (!bufState.open) return -1;
	if ((bufState.bufState0 == BUF_EMPTY) ||
		(bufState.bufState1 == BUF_EMPTY)) {
			return bufState.bufSizeInBytes;
	}
	return 0;
}

int snd_PlaySamplesFromAtLength(int frameCount, int arrayIndex, int startIndex) {
	SndDoubleBufferPtr buf;
	int framesWritten;

	if (!bufState.open) return -1;

	if (bufState.bufState0 == BUF_EMPTY) {
		buf = dblBufHeader.dbhBufferPtr[0];
		bufState.bufState0 = BUF_FULL;
	} else {
		if (bufState.bufState1 == BUF_EMPTY) {
			buf = dblBufHeader.dbhBufferPtr[1];
			bufState.bufState1 = BUF_FULL;
		} else {
			return 0;  /* neither buffer is available */
		}
	}

	if (bufState.frameCount < frameCount) {
		framesWritten = bufState.frameCount;
	} else {
		framesWritten = frameCount;
	}

	if (BYTES_PER_SAMPLE == 1) {  /* 8-bit samples */
		unsigned char *src, *dst, *end;
		src = (unsigned char *) (arrayIndex + startIndex);
		end = (unsigned char *) src + (framesWritten * (bufState.stereo ? 2 : 1));
		dst = (unsigned char *) &buf->dbSoundData[0];
		while (src < end) {
			*dst++ = *src++;
		}
	} else {  /* 16-bit samples */
		short int *src, *dst, *end;
		src = (short int *) (arrayIndex + (startIndex * 4));
		end = (short int *) (arrayIndex + ((startIndex + framesWritten) * 4));
		dst = (short int *) &buf->dbSoundData[0];
		if (bufState.stereo) {  /* stereo */
			while (src < end) {
				*dst++ = *src++;
			}
		} else {  /* mono */
			/* if mono, average the left and right channels of the source */
			while (src < end) {
				*dst++ = (*src++ + *src++) / 2;
			}
		}
	}
	return framesWritten;
}

int MixInSamples(int count, char *srcBufPtr, int srcStartIndex, char *dstBufPtr, int dstStartIndex) {
	int sample;

	if (BYTES_PER_SAMPLE == 1) {  /* 8-bit samples */
		unsigned char *src, *dst, *end;
		src = (unsigned char *) srcBufPtr + srcStartIndex;
		end = (unsigned char *) srcBufPtr + (count * (bufState.stereo ? 2 : 1));
		dst = (unsigned char *) dstBufPtr + dstStartIndex;
		while (src < end) {
			sample = *dst + (*src++ - 128);
			if (sample > 255) sample = 255;
			if (sample < 0) sample = 0;
			*dst++ = sample;
		}
	} else {  /* 16-bit samples */
		short int *src, *dst, *end;
		src = (short int *) (srcBufPtr + (srcStartIndex * 4));
		end = (short int *) (srcBufPtr + ((srcStartIndex + count) * 4));
		if (bufState.stereo) {  /* stereo */
			dst = (short int *) (dstBufPtr + (dstStartIndex * 4));
			while (src < end) {
				sample = *dst + *src++;
				if (sample > 32767) sample = 32767;
				if (sample < -32767) sample = -32767;
				*dst++ = sample;
			}
		} else {  /* mono */
			/* if mono, average the left and right channels of the source */
			dst = (short int *) (dstBufPtr + (dstStartIndex * 2));
			while (src < end) {
				sample = *dst + ((*src++ + *src++) / 2);
				if (sample > 32767) sample = 32767;
				if (sample < -32767) sample = -32767;
				*dst++ = sample;
			}
		}
	}
}

int snd_InsertSamplesFromLeadTime(int frameCount, int srcBufPtr, int samplesOfLeadTime) {
	SndDoubleBufferPtr bufPlaying, otherBuf;
	int samplesInserted, startSample, count;

	if (!bufState.open) return -1;

	if (bufState.bufState0 == BUF_PLAYING) {
		bufPlaying = dblBufHeader.dbhBufferPtr[0];
		otherBuf = dblBufHeader.dbhBufferPtr[1];
	} else {
		bufPlaying = dblBufHeader.dbhBufferPtr[1];
		otherBuf = dblBufHeader.dbhBufferPtr[0];
	}

	samplesInserted = 0;

	/* mix as many samples as can fit into the remainder of the currently playing buffer */
	startSample =
		((bufState.sampleRate * (ioMicroMSecs() - bufState.lastFlipTime)) / 1000) + samplesOfLeadTime;
	if (startSample < bufState.frameCount) {
		count = bufState.frameCount - startSample;
		if (count > frameCount) count = frameCount;
		MixInSamples(count, (char *) srcBufPtr, 0, &bufPlaying->dbSoundData[0], startSample);
		samplesInserted = count;
	}

	/* mix remaining samples into the inactive buffer */
	count = bufState.frameCount;
	if (count > (frameCount - samplesInserted)) {
		count = frameCount - samplesInserted;
	}
	MixInSamples(count, (char *) srcBufPtr, samplesInserted, &otherBuf->dbSoundData[0], 0);
	return samplesInserted + count;
}

int snd_PlaySilence(void) {
	if (!bufState.open) return -1;

	if (bufState.bufState0 == BUF_EMPTY) {
		FillBufferWithSilence(dblBufHeader.dbhBufferPtr[0]);
		bufState.bufState0 = BUF_FULL;
	} else {
		if (bufState.bufState1 == BUF_EMPTY) {
			FillBufferWithSilence(dblBufHeader.dbhBufferPtr[1]);
			bufState.bufState1 = BUF_FULL;
		} else {
			return 0;  /* neither buffer is available */
		}
	}
	return bufState.bufSizeInBytes;
}

int snd_Start(int frameCount, int samplesPerSec, int stereo, int semaIndex) {
	OSErr				err;
	SndDoubleBufferPtr	buffer;
	int					bytesPerFrame, bufferBytes, i;

	bytesPerFrame			= stereo ? 2 * BYTES_PER_SAMPLE : BYTES_PER_SAMPLE;
	bufferBytes				= ((frameCount * bytesPerFrame) / 8) * 8;
		/* Note: Must round bufferBytes down to an 8-byte boundary to avoid clicking!!! */

	if (bufState.open) {
		/* still open from last time; clean up before continuing */
		snd_Stop();
	}

	bufState.open			= false;  /* set to true if successful */
	bufState.stereo			= stereo;
	bufState.frameCount		= bufferBytes / bytesPerFrame;
	bufState.sampleRate		= samplesPerSec;
	bufState.lastFlipTime	= ioMicroMSecs();
	bufState.playSemaIndex	= semaIndex;
	bufState.bufSizeInBytes	= bufferBytes;
	bufState.bufState0		= BUF_EMPTY;
	bufState.bufState1		= BUF_EMPTY;
	bufState.done			= false;

	dblBufHeader.dbhNumChannels		= stereo ? 2 : 1;
	dblBufHeader.dbhSampleSize		= BYTES_PER_SAMPLE * 8;
	dblBufHeader.dbhCompressionID	= 0;
	dblBufHeader.dbhPacketSize		= 0;
	dblBufHeader.dbhSampleRate		= samplesPerSec << 16; /* convert to fixed point */
	dblBufHeader.dbhDoubleBack		= NewSndDoubleBackProc(DoubleBack);

	chan = NULL;
	err = SndNewChannel(&chan, sampledSynth, 0, NULL);
	if (err != noErr) return false; /* could not open sound channel */

	for (i = 0; i < 2; i++) {
		buffer = (SndDoubleBufferPtr) NewPtrClear(sizeof(SndDoubleBuffer) + bufState.bufSizeInBytes);
		if (buffer == NULL) return false; /* could not allocate memory for a buffer */
		buffer->dbNumFrames		= bufState.frameCount;
		buffer->dbFlags			= dbBufferReady;
		buffer->dbUserInfo[0]	= (long) &bufState;
		buffer->dbUserInfo[1]	= i;
		FillBufferWithSilence(buffer);

		dblBufHeader.dbhBufferPtr[i] = buffer;
	}

	err = SndPlayDoubleBuffer(chan, &dblBufHeader);
	if (err != noErr) return false; /* could not play double buffer */

	bufState.open = true;
	return true;
}

int snd_Stop(void) {
	OSErr				err;
	SndDoubleBufferPtr	buffer;
	SCStatus			status;
	long				i, junk;

	if (!bufState.open) return;
	bufState.open = false;

	bufState.done = true;
	while (true) {
		err = SndChannelStatus(chan, sizeof(status), &status);
		if (err != noErr) break; /* could not get channel status */
		if (!status.scChannelBusy) break;
		Delay(1, &junk);
	}
	SndDisposeChannel(chan, true);
	DisposeRoutineDescriptor(dblBufHeader.dbhDoubleBack);

	for (i = 0; i < 2; i++) {
		buffer = dblBufHeader.dbhBufferPtr[i];
		if (buffer != NULL) {
			DisposePtr((char *) buffer);
		}
		dblBufHeader.dbhBufferPtr[i] = NULL;
	}
	bufState.open = false;
}

/*** exported sound input functions ***/

int snd_SetRecordLevel(int level) {
	/* set the recording level to a value between 0 (minimum gain) and 1000. */
	Fixed inputGainArg;
	int err;

	if (!recordingInProgress || (level < 0) || (level > 1000)) {
		success(false);
		return;  /* noop if not recording */
	}

	inputGainArg = ((500 + level) << 16) / 1000;  /* gain is Fixed between 0.5 and 1.5 */
	err = SPBSetDeviceInfo(soundInputRefNum, siInputGain, &inputGainArg);
	/* don''t fail on error; hardware may not support setting the gain */
}

int snd_StartRecording(int desiredSamplesPerSec, int stereo, int semaIndex) {
	/* turn on sound recording, trying to use a sampling rate close to
	   the one specified. semaIndex is the index in the exportedObject
	   array of a semaphore to be signalled when input data is available. */
	Str255 deviceName = "";
	short automaticGainControlArg;
	Fixed inputGainArg;
	long  compressionTypeArg;
	short continuousArg;
	short sampleSizeArg;
	short channelCountArg;
	UnsignedFixed sampleRateArg;
	int err;

	err = SPBOpenDevice(deviceName, siWritePermission, &soundInputRefNum);
	if (err != noErr) {
		success(false);
		return;
	}

	/* try to initialize some optional parameters, but don''t fail if we can''t */
	automaticGainControlArg = false;
	SPBSetDeviceInfo(soundInputRefNum, siAGCOnOff, &automaticGainControlArg);
	inputGainArg = 1 << 16;  /* 1.0 in Fixed */
	SPBSetDeviceInfo(soundInputRefNum, siInputGain, &inputGainArg);
	compressionTypeArg = ''NONE'';
	SPBSetDeviceInfo(soundInputRefNum, siCompressionType, &compressionTypeArg);

	continuousArg = true;
	err = SPBSetDeviceInfo(soundInputRefNum, siContinuous, &continuousArg);
	if (err != noErr) {
		success(false);
		SPBCloseDevice(soundInputRefNum);
		return;
	}

	sampleSizeArg = 16;
	err = SPBSetDeviceInfo(soundInputRefNum, siSampleSize, &sampleSizeArg);
	if (err != noErr) {
		/* use 8-bit samples */
		sampleSizeArg = 8;
		err = SPBSetDeviceInfo(soundInputRefNum, siSampleSize, &sampleSizeArg);
		if (err != noErr) {
			success(false);
			SPBCloseDevice(soundInputRefNum);
			return;
		}
	}

	channelCountArg = stereo ? 2 : 1;
	err = SPBSetDeviceInfo(soundInputRefNum, siNumberChannels, &channelCountArg);
	if (err != noErr) {
		success(false);
		SPBCloseDevice(soundInputRefNum);
		return;
	}

	/* try to set the client''s desired sample rate */
	sampleRateArg = desiredSamplesPerSec << 16;
	err = SPBSetDeviceInfo(soundInputRefNum, siSampleRate, &sampleRateArg);
	if (err != noErr) {
		/* if client''s rate fails, try the nearest common sampling rates in {11025, 22050, 44100} */
		if (desiredSamplesPerSec <= 16538) {
			sampleRateArg = 11025 << 16;
		} else {
			if (desiredSamplesPerSec <= 33075) {
				sampleRateArg = 22050 << 16;
			} else {
				sampleRateArg = 44100 << 16;
			}
		}
		/* even if following fails, recording can go on at the default sample rate */
		SPBSetDeviceInfo(soundInputRefNum, siSampleRate, &sampleRateArg);
	}

	recordBuffer1.paramBlock.inRefNum = soundInputRefNum;
	recordBuffer1.paramBlock.count = RECORD_BUFFER_SIZE;
	recordBuffer1.paramBlock.milliseconds = 0;
	recordBuffer1.paramBlock.bufferLength = RECORD_BUFFER_SIZE;
	recordBuffer1.paramBlock.bufferPtr = recordBuffer1.samples;
	recordBuffer1.paramBlock.completionRoutine = NewSICompletionProc(FlipRecordBuffers);
	recordBuffer1.paramBlock.interruptRoutine = nil;
	recordBuffer1.paramBlock.userLong = (long) &recordBuffer2;  /* pointer to other buffer */
	recordBuffer1.paramBlock.error = noErr;
	recordBuffer1.paramBlock.unused1 = 0;
	recordBuffer1.stereo = stereo;
	recordBuffer1.bytesPerSample = sampleSizeArg == 8 ? 1 : 2;
	recordBuffer1.recordSemaIndex = semaIndex;
	recordBuffer1.readIndex = RECORD_BUFFER_SIZE;

	recordBuffer2.paramBlock.inRefNum = soundInputRefNum;
	recordBuffer2.paramBlock.count = RECORD_BUFFER_SIZE;
	recordBuffer2.paramBlock.milliseconds = 0;
	recordBuffer2.paramBlock.bufferLength = RECORD_BUFFER_SIZE;
	recordBuffer2.paramBlock.bufferPtr = recordBuffer2.samples;
	recordBuffer2.paramBlock.completionRoutine = NewSICompletionProc(FlipRecordBuffers);
	recordBuffer2.paramBlock.interruptRoutine = nil;
	recordBuffer2.paramBlock.userLong = (long) &recordBuffer1;  /* pointer to other buffer */
	recordBuffer2.paramBlock.error = noErr;
	recordBuffer2.paramBlock.unused1 = 0;
	recordBuffer2.stereo = stereo;
	recordBuffer2.bytesPerSample = sampleSizeArg == 8 ? 1 : 2;
	recordBuffer2.recordSemaIndex = semaIndex;
	recordBuffer2.readIndex = RECORD_BUFFER_SIZE;

	err = SPBRecord(&recordBuffer1.paramBlock, true);
	if (err != noErr) {
		success(false);
		SPBCloseDevice(soundInputRefNum);
		return;
	}

	recordingInProgress = true;
}

int snd_StopRecording(void) {
	/* turn off sound recording */
	int err;

	if (!recordingInProgress) return;  /* noop if not recording */

	err = SPBStopRecording(soundInputRefNum);
	if (err != noErr) success(false);
	SPBCloseDevice(soundInputRefNum);

	DisposeRoutineDescriptor(recordBuffer1.paramBlock.completionRoutine);
	recordBuffer1.paramBlock.completionRoutine = nil;
	DisposeRoutineDescriptor(recordBuffer2.paramBlock.completionRoutine);
	recordBuffer2.paramBlock.completionRoutine = nil;

	recordBuffer1.recordSemaIndex = 0;
	recordBuffer2.recordSemaIndex = 0;
	recordingInProgress = false;
}

double snd_GetRecordingSampleRate(void) {
	/* return the actual recording rate; fail if not currently recording */
	UnsignedFixed sampleRateArg;
	int err;

	if (!recordingInProgress) {
		success(false);
		return 0.0;
	}

	err = SPBGetDeviceInfo(soundInputRefNum, siSampleRate, &sampleRateArg);
	if (err != noErr) {
		success(false);
		return 0.0;
	}
	return  (double) ((sampleRateArg >> 16) & 0xFFFF) +
			((double) (sampleRateArg & 0xFFFF) / 65536.0);
}

int snd_RecordSamplesIntoAtLength(int buf, int startSliceIndex, int bufferSizeInBytes) {
	/* if data is available, copy as many sample slices as possible into the
	   given buffer starting at the given slice index. do not write past the
	   end of the buffer, which is buf + bufferSizeInBytes. return the number
	   of slices (not bytes) copied. a slice is one 16-bit sample in mono
	   or two 16-bit samples in stereo. */
	int bytesPerSlice = (recordBuffer1.stereo ? 4 : 2);
	char *nextBuf = (char *) buf + (startSliceIndex * bytesPerSlice);
	char *bufEnd = (char *) buf + bufferSizeInBytes;
	char *src, *srcEnd;
	RecordBuffer recBuf = nil;
	int bytesCopied;

	if (!recordingInProgress) {
		success(false);
		return 0;
	}

	/* select the buffer with unread samples, if any */
	recBuf = nil;
	if (recordBuffer1.readIndex < RECORD_BUFFER_SIZE) recBuf = &recordBuffer1;
	if (recordBuffer2.readIndex < RECORD_BUFFER_SIZE) recBuf = &recordBuffer2;
	if (recBuf == nil) return 0;  /* no samples available */

	/* copy samples into the client''s buffer */
	src = &recBuf->samples[0] + recBuf->readIndex;
	srcEnd = &recBuf->samples[RECORD_BUFFER_SIZE];
	if (recBuf->bytesPerSample == 1) {
		while ((src < srcEnd) && (nextBuf < bufEnd)) {
			/* convert 8-bit sample to 16-bit sample */
			*nextBuf++ = (*src++) - 128;  /* convert from [0-255] to [-128-127] */
			*nextBuf++ = 0;  /* low-order byte is zero */
		}
	} else {
		while ((src < srcEnd) && (nextBuf < bufEnd)) {
			*nextBuf++ = *src++;
		}
	}
	recBuf->readIndex = src - &recBuf->samples[0];  /* update read index */

	/* return the number of slices copied */
	bytesCopied = (int) nextBuf - (buf + (startSliceIndex * bytesPerSlice));
	return bytesCopied / bytesPerSlice;
}
'.
