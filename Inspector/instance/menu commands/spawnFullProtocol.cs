spawnFullProtocol
	"Spawn a window showing full protocol for the receiver's selection"

	| objectToRepresent |
	objectToRepresent := self selectionIndex == 0 ifTrue: [object] ifFalse: [self selection].
	ProtocolBrowser openFullProtocolForClass: objectToRepresent class