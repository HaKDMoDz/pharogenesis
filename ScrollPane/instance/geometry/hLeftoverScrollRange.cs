hLeftoverScrollRange
	"Return the entire scrolling range minus the currently viewed area."
	| w |
	scroller hasSubmorphs ifFalse:[^0].
	w _  bounds width.
	self vIsScrollbarShowing ifTrue:[ w _ w - self scrollBarThickness ].
	^ (self hTotalScrollRange - w roundTo: self scrollDeltaHeight) max: 0
