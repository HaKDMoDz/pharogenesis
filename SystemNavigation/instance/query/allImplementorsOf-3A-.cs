allImplementorsOf: aSelector 
	"Answer a SortedCollection of all the methods that implement the message 
	aSelector."
	| aCollection |
	aCollection _ SortedCollection new.
	Cursor wait
		showWhile: [self
				allBehaviorsDo: [:class | (class includesSelector: aSelector)
						ifTrue: [aCollection
								add: (MethodReference new setStandardClass: class methodSymbol: aSelector)]]].
	^ aCollection