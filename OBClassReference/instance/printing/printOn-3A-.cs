printOn: aStream
	aStream nextPutAll: 'OBClassReference'.
	aStream nextPut: $<.
	aStream nextPutAll: name.
	isMeta ifTrue: [aStream nextPutAll: ' class'].
	aStream nextPut: $>.