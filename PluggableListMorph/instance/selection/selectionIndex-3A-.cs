selectionIndex: index
	"Called internally to select the index-th item."
	| row |
	self unhighlightSelection.
	row := index ifNil: [ 0 ].
	row := row min: self getListSize.  "make sure we don't select past the end"
	self listMorph selectedRow: row.
	self highlightSelection.
	self scrollSelectionIntoView.