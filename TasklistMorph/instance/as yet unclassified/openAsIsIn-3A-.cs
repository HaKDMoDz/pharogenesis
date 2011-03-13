openAsIsIn: aWorld
	"Update the layout after opening."

	aWorld addMorphCentered: self.
	self allMorphs do: [:m | m layoutChanged].
	aWorld startSteppingSubmorphsOf: self.
	self wantsKeyboardFocus
		ifTrue: [self takeKeyboardFocus]