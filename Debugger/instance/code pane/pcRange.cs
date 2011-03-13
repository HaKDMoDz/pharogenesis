pcRange
	"Answer the indices in the source code for the method corresponding to 
	the selected context's program counter value."

	| i pc end |
	(selectingPC and: [contextStackIndex ~= 0])
		ifFalse: [^1 to: 0].
	sourceMap ifNil:
		[sourceMap := theMethodNode sourceMap.
		tempNames := theMethodNode tempNames].
	(sourceMap size = 0 or: [ self selectedContext isDead ]) ifTrue: [^1 to: 0].
	Smalltalk at: #RBProgramNode ifPresent:[:nodeClass|
		(theMethodNode isKindOf: nodeClass) ifTrue: [
			pc := contextStackIndex = 1
				ifTrue: [self selectedContext pc]
				ifFalse: [self selectedContext previousPc].
			i := sourceMap findLast:[:pcRange | pcRange key <= pc].
			i = 0 ifTrue:[^ 1 to: 0].
			^ (sourceMap at: i) value
		].
	].
	pc:= self selectedContext pc -
		(("externalInterrupt" true and: [contextStackIndex=1])
			ifTrue: [1]
			ifFalse: [2]).
	i := sourceMap indexForInserting: (Association key: pc value: nil).
	i < 1 ifTrue: [^1 to: 0].
	i > sourceMap size
		ifTrue:
			[end := sourceMap inject: 0 into:
				[:prev :this | prev max: this value last].
			^ end+1 to: end].
	^(sourceMap at: i) value