toggleListIndex: newListIndex

	listIndex ~= 0 ifTrue: [listSelections at: listIndex put: false].
	newListIndex ~= 0 ifTrue: [listSelections at: newListIndex put: true].
	listIndex _ newListIndex.
	self changed: #listIndex.
	self contentsChanged