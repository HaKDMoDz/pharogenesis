line1: line1

	| chopped label label2 tail headIndex |
	(line1 select: [:e | e = $:]) size >= 2 ifTrue: [^ self].
	self removeAllMorphs.
	headIndex _ line1 indexOf: $: ifAbsent: [line1 indexOf: Character space ifAbsent: [0]].

	chopped _ line1 copyFrom: 1 to: headIndex - 1.
	tail _ line1 copyFrom: chopped size + 2 to: line1 size.
	label _ 	StringMorph contents: (chopped) translated font: ScriptingSystem fontForTiles.
	label2 _ StringMorph contents: tail translated font: ScriptingSystem fontForTiles.

	self addMorphBack: label.
	self addMorphBack: patchTile.
	self addMorphBack: label2.
