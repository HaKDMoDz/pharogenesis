open
	| openCount |
	openCount := 0.
	self dependents
		do: [:d | (d isSystemWindow)
				ifTrue: [openCount := openCount + 1]].
	openCount = 0
		ifTrue: [self openLabel: 'Transcript']
		ifFalse: [self openLabel: 'Transcript #' , (openCount + 1) printString]