alignCentersVertically
	"Make every morph in the selection have the same horizontal center as the topmost item."

	| minTop topMost |
	selectedItems size > 1 ifFalse: [^ self].
	minTop _ (selectedItems collect: [:itm | itm top]) min.
	topMost _ selectedItems detect: [:m | m top = minTop].
	selectedItems do:
		[:itm | itm center: (topMost center x @ itm center y)].

	self changed
