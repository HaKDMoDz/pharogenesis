lineNumber
	| index start stop pos |
	pos := sourceStream position.
	pos >= eolPositions last ifTrue: [^eolPositions size].
	start := 1.
	stop := eolPositions size.
	[start + 1 < stop] whileTrue: 
			[index := (start + stop) // 2.
			(eolPositions at: index) <= pos 
				ifTrue: [start := index]
				ifFalse: [stop := index]].
	^start