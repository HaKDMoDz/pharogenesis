on: anObject list: getListSel selected: getSelectionSel changeSelected: setSelectionSel
	"Create a 'pluggable' list view on the given model parameterized by the given message selectors. See ListView>>aboutPluggability comment."

	^ self new
		on: anObject
		list: getListSel
		selected: getSelectionSel
		changeSelected: setSelectionSel
		menu: nil
		keystroke: #arrowKey:from:		"default"