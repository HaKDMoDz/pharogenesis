showChangeSetCategory: aChangeSetCategory
	"Show the given change-set category"
	
	changeSetCategory _ aChangeSetCategory.
	self changed: #changeSetList.
	(self changeSetList includes: myChangeSet name) ifFalse:
			[self showChangeSet: (ChangeSorter changeSetNamed: self changeSetList first)].
	self changed: #relabel