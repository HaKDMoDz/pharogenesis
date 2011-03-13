readFrom: stream
	| date time |
	stream skipSeparators.
	date := Date readFrom: stream.
	stream skipSeparators.
	time := Time readFrom: stream.
	^self 
		date: date
		time: time