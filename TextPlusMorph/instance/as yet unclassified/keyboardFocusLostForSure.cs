keyboardFocusLostForSure

	editor ifNotNil: [
		self selectionChanged.
		self paragraph selectionStart: nil selectionStop: nil.
		editor _ nil
	].


