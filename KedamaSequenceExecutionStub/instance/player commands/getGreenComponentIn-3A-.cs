getGreenComponentIn: aPatch

	| i pix |
	i _ self index.
	pix _ aPatch costume renderedMorph pixelAtX: ((turtles arrays at: 2) at: i) y: ((turtles arrays at: 3) at: i).
	^ (pix bitShift: -8) bitAnd: 16rFF.
