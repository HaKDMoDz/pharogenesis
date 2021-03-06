selectedClassOrMetaClass
	"Careful, the class may have been removed!"

	| cName tName |
	currentClassName ifNil: [^ nil].
	(currentClassName endsWith: ' class')
		ifTrue: [cName := (currentClassName copyFrom: 1 to: currentClassName size-6) asSymbol.
				^ (Smalltalk at: cName ifAbsent: [^nil]) class].
	(currentClassName endsWith: ' classTrait')
		ifTrue: [tName := (currentClassName copyFrom: 1 to: currentClassName size-11) asSymbol.
				^ (Smalltalk at: tName ifAbsent: [^nil]) classTrait].
	cName := currentClassName asSymbol.
	^ Smalltalk at: cName ifAbsent: [nil]