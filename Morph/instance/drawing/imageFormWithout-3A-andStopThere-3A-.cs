imageFormWithout: stopMorph andStopThere: stopThere
	"Like imageForm, except it does not display stopMorph,
	and it will not display anything above it if stopThere is true.
	Returns a pair of the imageForm and a boolean that is true
		if it has hit stopMorph, and display should stop."
	| canvas rect |
	rect _ self fullBounds.
	canvas _ ColorPatchCanvas extent: rect extent depth: Display depth.
	canvas stopMorph: stopMorph.
	canvas doStop: stopThere.
	canvas translateBy: rect topLeft negated during:[:tempCanvas| self fullDrawOn: tempCanvas].
	^ Array with: (canvas form offset: rect topLeft)
			with: canvas foundMorph