addPatchTile

	| m1 desiredW m2 label |
	m1 _ TilePadMorph new.
	label _ 	StringMorph contents: (operatorOrExpression asString upTo: $:) translated font: ScriptingSystem fontForTiles.

	m2 _ TileMorph new.
	m2 extent: 20@22.
	m2 minWidth: 20.
	m1 extent: (m2 extent + (2@2)).
	m1 setType: #Patch.
	m1 addMorph: m2.

	desiredW _ m1 width + 6.
	self extent: (desiredW max: self basicWidth) @ self class defaultH.
	m1 position: (bounds center x - (m1 width // 2)) @ (bounds top + 1).

	self addMorphBack: m1.
	self addMorphFront: label.
	patchTile _ m1.
