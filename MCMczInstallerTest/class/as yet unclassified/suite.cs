suite
	^ (Smalltalk hasClassNamed: #MczInstaller)
		ifTrue: [super suite]
		ifFalse: [TestSuite new name: self name asString]