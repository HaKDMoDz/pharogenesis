addTree: aFileNameOrDirectory match: aBlock 
	| nameSize |
	nameSize := aFileNameOrDirectory isString
				ifTrue: [aFileNameOrDirectory size]
				ifFalse: [aFileNameOrDirectory pathName size].
	^ self
		addTree: aFileNameOrDirectory
		removingFirstCharacters: nameSize + 1
		match: aBlock