embeddedInMorphicWindowLabeled: labelString
	| window |
	window _ (SystemWindow labelled: labelString) model: nil.
	window setStripeColorsFrom: nil defaultBackgroundColor.
	window addMorph: self frame: (0@0 extent: 1@1).
	^ window