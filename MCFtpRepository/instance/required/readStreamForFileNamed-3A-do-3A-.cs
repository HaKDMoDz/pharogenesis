readStreamForFileNamed: aString do: aBlock
	| stream |
	^ self clientDo:
		[:client |
		client binary.
		stream := RWBinaryOrTextStream on: String new.
		stream nextPutAll: (client getFileNamed: aString).
		aBlock value: stream reset]