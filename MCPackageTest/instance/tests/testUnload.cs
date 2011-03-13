testUnload
	| mock |
	self mockPackage unload.
	self deny: (Smalltalk hasClassNamed: #MCMockClassA).
	self deny: (MCSnapshotTest includesSelector: #mockClassExtension).

	mock := (Smalltalk at: #MCMock).
	self assert: (mock subclasses detect: [:c | c name = #MCMockClassA] ifNone: []) isNil