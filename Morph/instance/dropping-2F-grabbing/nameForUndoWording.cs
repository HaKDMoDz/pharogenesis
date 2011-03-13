nameForUndoWording
	"Return wording appropriate to the receiver for use in an undo-related menu item (and perhaps elsewhere)"

	| aName |
	aName := self knownName ifNil: [self renderedMorph class name].
	^ aName truncateTo: 24