at: index put: anInteger
	| word |
	<primitive: 166>
	anInteger < 0
		ifTrue:["word _ 16r100000000 + anInteger"
				word _ (anInteger + 1) negated bitInvert32]
		ifFalse:[word _ anInteger].
	self  basicAt: index put: word.
	^anInteger