scanVersionsOf: method class: class meta: meta category: cat selector: selector
	| position prevPos prevFileIndex preamble tokens sourceFilesCopy stamp changeList file |
	changeList _ OrderedCollection new.
	position _ method filePosition.
	sourceFilesCopy _ SourceFiles collect:[:x | x ifNotNil:[x readOnlyCopy]].
	method fileIndex == 0 ifTrue: [^ nil].
	file _ sourceFilesCopy at: method fileIndex.
	[position notNil & file notNil] whileTrue:[
		file position: (0 max: position-150).  "Skip back to before the preamble"
		preamble _ method getPreambleFrom: file at: (0 max: position - 3).
		"Preamble is likely a linked method preamble, if we're in
			a changes file (not the sources file).  Try to parse it
			for prior source position and file index"
		prevPos _ nil.
		stamp _ ''.
		(preamble findString: 'methodsFor:' startingAt: 1) > 0
			ifTrue: [tokens _ Scanner new scanTokens: preamble]
			ifFalse: [tokens _ Array new  "ie cant be back ref"].
		((tokens size between: 7 and: 8)
			and: [(tokens at: tokens size-5) = #methodsFor:]) ifTrue:[
				(tokens at: tokens size-3) = #stamp: ifTrue:[
					"New format gives change stamp and unified prior pointer"
					stamp _ tokens at: tokens size-2.
					prevPos _ tokens last.
					prevFileIndex _ sourceFilesCopy fileIndexFromSourcePointer: prevPos.
					prevPos _ sourceFilesCopy filePositionFromSourcePointer: prevPos.
				] ifFalse: ["Old format gives no stamp; prior pointer in two parts"
					prevPos _ tokens at: tokens size-2.
					prevFileIndex _ tokens last.
				].
				(prevPos = 0 or: [prevFileIndex = 0]) ifTrue: [prevPos _ nil]
			].
		((tokens size between: 5 and: 6)
			and: [(tokens at: tokens size-3) = #methodsFor:]) ifTrue:[
				(tokens at: tokens size-1) = #stamp: ifTrue: [
					"New format gives change stamp and unified prior pointer"
					stamp _ tokens at: tokens size.
			]
		].
 		changeList add: (ChangeRecord new file: file position: position type: #method
						class: class name category: cat meta: meta stamp: stamp).
		position _ prevPos.
		prevPos notNil ifTrue:[file _ sourceFilesCopy at: prevFileIndex].
	].
	sourceFilesCopy do: [:x | x ifNotNil:[x close]].
	^changeList