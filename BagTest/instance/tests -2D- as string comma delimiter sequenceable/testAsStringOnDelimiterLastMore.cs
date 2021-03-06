testAsStringOnDelimiterLastMore

	| delim multiItemStream result last allElementsAsString tmp |
	
	delim := ', '.
	last := 'and'.
	result:=''.
	tmp := self nonEmpty collect: [:each | each asString].
	multiItemStream := ReadWriteStream on:result.
	self nonEmpty  asStringOn: multiItemStream delimiter: ', ' last: last.
	
	allElementsAsString:=(result findBetweenSubStrs: ', ' ).
	1 to: allElementsAsString size do:
		[:i | 
		i<(allElementsAsString size-1 ) | i= allElementsAsString size
			ifTrue: [self assert: (tmp occurrencesOf:(allElementsAsString at:i))=(allElementsAsString 			occurrencesOf:(allElementsAsString at:i))].
		i=(allElementsAsString size-1)
			ifTrue:[ self assert: (allElementsAsString at:i)=('and')].
			].
