mouseMove: evt 
	"Change the selection in response to moue-down drag"
	| dragBlock |
	dragBlock _ paragraph characterBlockAtPoint: (evt cursorPoint).
	dragBlock > pivotBlock
		ifTrue: [stopBlock _ dragBlock.  startBlock _ pivotBlock]
		ifFalse: [startBlock _ dragBlock.  stopBlock _ pivotBlock].
	paragraph selectionStart: startBlock selectionStop: stopBlock