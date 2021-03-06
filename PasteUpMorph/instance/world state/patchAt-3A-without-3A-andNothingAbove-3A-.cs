patchAt: patchRect without: stopMorph andNothingAbove: stopThere
	"Return a complete rendering of this patch of the display screen
	without stopMorph, and possibly without anything above it."

	| c |
	c := ColorPatchCanvas
		extent: patchRect extent
		depth: Display depth
		origin: patchRect topLeft negated
		clipRect: (0@0 extent: patchRect extent).
	c stopMorph: stopMorph.
	c doStop: stopThere.

	(self bounds containsRect: patchRect) ifFalse:
		["Need to fill area outside bounds with black."
		c form fillColor: Color black].
	(self bounds intersects: patchRect) ifFalse:
		["Nothing within bounds to show."
		^ c form].
	self fullDrawOn: c.
	stopThere ifFalse: [ self world handsReverseDo: [:h | h drawSubmorphsOn: c]].
	^c form
