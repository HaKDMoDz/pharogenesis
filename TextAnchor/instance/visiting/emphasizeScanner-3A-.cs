emphasizeScanner: aScanner
	self anchoredMorph ifNil: [ ^ self ].
	aScanner placeEmbeddedObject: self anchoredMorph.