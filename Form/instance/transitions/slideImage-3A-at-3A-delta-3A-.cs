slideImage: otherImage at: topLeft delta: delta
	"Display slideImage: (Form fromDisplay: (40@40 extent: 300@300)) reverse
		at: 40@40 delta: 3@-4"
	| bb nSteps clipRect |
	bb _ otherImage boundingBox.
	clipRect _ topLeft extent: otherImage extent.
	nSteps _ 1.
	delta x = 0 ifFalse: [nSteps _ nSteps max: (bb width//delta x abs) + 1].
	delta y = 0 ifFalse: [nSteps _ nSteps max: (bb height//delta y abs) + 1].
	1 to: nSteps do:
			[:i | self copyBits: bb from: otherImage
				at: delta*(i-nSteps) + topLeft
				clippingBox: clipRect rule: Form over fillColor: nil.
			Smalltalk forceDisplayUpdate]