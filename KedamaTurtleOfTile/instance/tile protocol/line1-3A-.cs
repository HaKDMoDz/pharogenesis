line1: line1

	| label |
	self removeAllMorphs.

	label _ 	StringMorph contents: 'turtleOf' font: ScriptingSystem fontForTiles.

	self addMorphBack: label.
	self addMorphBack: turtleTile.
