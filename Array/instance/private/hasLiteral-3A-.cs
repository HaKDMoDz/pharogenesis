hasLiteral: literal
	"Answer true if literal is identical to any literal in this array, even 
	if imbedded in further array structure. This method is only intended 
	for private use by CompiledMethod hasLiteralSymbol:"

	| lit |
	1 to: self size do: 
		[:index | 
		(lit _ self at: index) == literal ifTrue: [^ true].
		(lit class == Array and: [lit hasLiteral: literal]) ifTrue: [^ true]].
	^ false