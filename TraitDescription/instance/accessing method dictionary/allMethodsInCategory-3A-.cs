allMethodsInCategory: aName 
	"Answer a list of all the method categories of the receiver"
	
	| aColl |
	aColl := aName = ClassOrganizer allCategory
		ifTrue: [self organization allMethodSelectors]
		ifFalse: [self organization listAtCategoryNamed: aName].
	^aColl asSet asSortedArray

	"TileMorph allMethodsInCategory: #initialization"