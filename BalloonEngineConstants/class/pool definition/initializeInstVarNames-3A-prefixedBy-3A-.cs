initializeInstVarNames: aClass prefixedBy: aString

	| token value |
	aClass instVarNames doWithIndex:[:instVarName :index|
		token _ (aString, instVarName first asUppercase asString, (instVarName copyFrom: 2 to: instVarName size),'Index') asSymbol.
		value _ index - 1.
		(self bindingOf: token) ifNil:[self addClassVarName: token].
		(self bindingOf: token) value: value.
	].
	token _ (aString, aClass name,'Size') asSymbol.
	(self bindingOf: token) ifNil:[self addClassVarName: token].
	(self bindingOf: token) value: aClass instSize.