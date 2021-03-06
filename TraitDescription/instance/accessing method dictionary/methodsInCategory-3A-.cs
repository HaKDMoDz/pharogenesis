methodsInCategory: aName 
	"Answer a list of the methods of the receiver that are in category named aName"
	
	| aColl |
	aColl := aName = ClassOrganizer allCategory
		ifTrue: [self organization allMethodSelectors]
		ifFalse: [self organization listAtCategoryNamed: aName].
	^aColl asSet asSortedArray

	"TileMorph allMethodsInCategory: #initialization"