installAsTextStyle  "FontSetNewYork installAsTextStyle"
	| selectors |
	(TextConstants includesKey: self fontName) ifTrue:
		[(self confirm: 
self fontName , ' is already defined in TextConstants.
Do you want to replace that definition?')
			ifFalse: [^ self]].
	selectors _ (self class selectors select: [:s | s beginsWith: 'size']) asSortedCollection.
	TextConstants
		at: self fontName
		put: (TextStyle fontArray: (selectors collect: [:each | self perform: each]))