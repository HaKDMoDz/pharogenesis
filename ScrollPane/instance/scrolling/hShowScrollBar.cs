hShowScrollBar

	self hIsScrollbarShowing ifTrue: [^self].
	self hResizeScrollBar.
	self privateAddMorph: hScrollBar atIndex: 1.
	retractableScrollBar ifFalse: [self resetExtent].
