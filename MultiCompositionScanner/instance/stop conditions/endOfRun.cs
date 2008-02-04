endOfRun
	"Answer true if scanning has reached the end of the paragraph. 
	Otherwise step conditions (mostly install potential new font) and answer 
	false."

	| runLength |
	lastIndex = text size
	ifTrue:	[line stop: lastIndex.
			presentationLine stop: lastIndex - numOfComposition.
			spaceX := destX.
			line paddingWidth: rightMargin - destX.
			presentationLine paddingWidth: rightMargin - destX.
			^true]
	ifFalse:	[
			"(text at: lastIndex) charCode = 32 ifTrue: [destX := destX + spaceWidth]."
			runLength := (text runLengthFor: (lastIndex := lastIndex + 1)).
			runStopIndex := lastIndex + (runLength - 1).
			self setStopConditions.
			^false]
