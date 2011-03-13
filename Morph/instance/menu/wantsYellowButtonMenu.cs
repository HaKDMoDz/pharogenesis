wantsYellowButtonMenu
	"Answer true if the receiver wants a yellow button menu"
	self
		valueOfProperty: #wantsYellowButtonMenu
		ifPresentDo: [:value | ^ value].
	""
	self isInSystemWindow
		ifTrue: [^ false].""
	(Preferences noviceMode
			and: [self isInDockingBar])
		ifTrue: [^ false].""
	^ Preferences generalizedYellowButtonMenu