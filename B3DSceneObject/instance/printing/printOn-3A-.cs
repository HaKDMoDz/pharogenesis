printOn: aStream
	super printOn: aStream.
	aStream
		nextPut:$(;
		print: self name;
		nextPut: $).