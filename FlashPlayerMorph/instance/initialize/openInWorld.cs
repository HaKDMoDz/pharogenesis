openInWorld
	| window extent |
	self localBounds: localBounds.
	extent := bounds extent.
	window := FlashPlayerWindow labelled:'Flash Player' translated.
	window model: (FlashPlayerModel player: self).
	window addMorph: self frame:(0@0 corner: 1@1).
	window openInWorldExtent: extent