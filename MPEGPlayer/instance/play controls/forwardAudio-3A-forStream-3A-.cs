forwardAudio: aNumber forStream: aStream
	| newLocation |

	self hasAudio ifFalse: [^self].
	newLocation := (((self currentAudioSampleForStream: aStream) + aNumber) min: (self audioSamples: aStream)) max: 0 .
	self currentAudioSampleForStream: aStream put: newLocation