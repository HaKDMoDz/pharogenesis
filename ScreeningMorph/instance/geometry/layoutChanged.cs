layoutChanged

	screenForm _ nil.
	submorphs size >= 2
		ifTrue: [self disableDragNDrop]
		ifFalse: [self enableDragNDrop].
	submorphs size = 2 ifTrue:
		[bounds _ ((self sourceMorph bounds merge: self screenMorph bounds) expandBy: 4)].
	^ super layoutChanged