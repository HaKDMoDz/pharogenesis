line1: line1

	| label |
	self removeAllMorphs.

	label _ 	StringMorph contents: 'turtleAt' font: ScriptingSystem fontForTiles.

	self addMorphBack: label.
	self addMorphBack: turtleTile.
