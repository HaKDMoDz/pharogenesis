playAudioStream: aStream

	self hasAudio ifFalse: [^self].
	self setupStream: aStream.
	self startAudioPlayerProcess: aStream.