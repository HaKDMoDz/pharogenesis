messageListKey: aChar from: view
	aChar == $b ifTrue: [^ self browseMethodFull].
	super messageListKey: aChar from: view