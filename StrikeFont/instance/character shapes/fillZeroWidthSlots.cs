fillZeroWidthSlots
	| nullGlyph |
	"Note: this is slow because it copies the font once for every replacement."

	nullGlyph _ (Form extent: 1@glyphs height) fillGray.
	"Now fill the empty slots with narrow box characters."
	minAscii to: maxAscii do:
		[:i | (self widthOf: (Character value: i)) = 0 ifTrue:
			[self characterFormAt: (Character value: i) put: nullGlyph]].
