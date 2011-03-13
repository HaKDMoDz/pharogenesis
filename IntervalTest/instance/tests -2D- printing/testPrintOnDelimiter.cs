testPrintOnDelimiter
	| aStream result allElementsAsString |
	result:=''.
	aStream:= ReadWriteStream on: result.
	
	self nonEmpty printOn: aStream delimiter: ', ' .
	
	allElementsAsString:=(result findBetweenSubStrs: ', ' ).
	1 to: allElementsAsString size do:
		[:i | 
		self assert: (allElementsAsString at:i)=((self nonEmpty at:i)asString).
			].