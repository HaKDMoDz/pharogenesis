measureString: aString inFont: aFont from: startIndex to: stopIndex 
	"WARNING: In order to use this method the receiver has to be set up using #initializeStringMeasurer"
	destX := destY := lastIndex := 0.
	xTable := aFont xTable.
	self 
		scanCharactersFrom: startIndex
		to: stopIndex
		in: aString
		rightX: 999999
		stopConditions: stopConditions
		kern: 0.
	^ destX