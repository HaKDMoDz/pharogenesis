openPath: anArray 
	| found |
	anArray isEmpty
		ifTrue: [^ container setSelectedMorph: nil].
	found _ nil.
	self
		withSiblingsDo: [:each | found
				ifNil: [(each complexContents asString = anArray first
							or: [anArray first isNil])
						ifTrue: [found _ each]]].
	found
		ifNil: ["try again with no case sensitivity"
			self
				withSiblingsDo: [:each | found
						ifNil: [(each complexContents asString sameAs: anArray first)
								ifTrue: [found _ each]]]].
	found
		ifNotNil: [found isExpanded
				ifFalse: [found toggleExpandedState.
					container adjustSubmorphPositions].
			found changed.
			anArray size = 1
				ifTrue: [^ container setSelectedMorph: found].
			^ found firstChild
				ifNil: [container setSelectedMorph: nil]
				ifNotNil: [found firstChild openPath: anArray allButFirst]].
	^ container setSelectedMorph: nil