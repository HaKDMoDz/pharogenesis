testPrintOnDelimiterLast
	| delim emptyStream last oneItemStream multiItemStream |
	delim := ', '.
	last := ' & '.
	{OrderedCollection new. Set new.} do:
		[ :coll |
		emptyStream := ReadWriteStream on: ''.
		coll printOn: emptyStream delimiter: delim last: last.
		self assert: emptyStream contents = ''.

		coll add: 1.
		oneItemStream := ReadWriteStream on: ''.
		coll printOn: oneItemStream delimiter: delim last: last.
		self assert: oneItemStream contents = '1'.

		coll add: 2; add: 3.
		multiItemStream := ReadWriteStream on: ''.
		coll printOn: multiItemStream delimiter: ', ' last: last.
		self assert: multiItemStream contents = '1'', ''2'' & ''3'.]