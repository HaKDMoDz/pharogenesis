writeOnGZIPByteStream: aStream 
	"We only intend this for non-pointer arrays.  Do nothing if I contain pointers."

	self class isPointers | self class isWords not ifTrue: [^ super writeOnGZIPByteStream: aStream].
		"super may cause an error, but will not be called."
	
	aStream nextPutAllWordArray: self