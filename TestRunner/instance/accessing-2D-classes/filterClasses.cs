filterClasses
	| pattern |
	pattern := UIManager default 
		request: 'Pattern to select tests:' 
		initialAnswer: '*'.
	pattern isNil ifTrue: [ ^ self ].
	classesSelected := (classes select: [ :each | 
		pattern match: each name ]) asSet.
	self changed: #classSelected; changed: #hasRunnable.