buildGraphAreaIn: frame
	| r y |
	graphArea _ RectangleMorph
		newBounds: ((frame left + 40) @ (frame top + 40)
		corner: (frame right+1) @ (frame bottom - 50))
		color: Color lightGreen lighter lighter.
	graphArea borderWidth: 1.
	self addMorph: graphArea.
	(envelope updateSelector = #pitch: and: [envelope scale <= 2.0]) ifTrue:
		["Show half-steps"
		r _ graphArea innerBounds.
		0.0 to: 1.0 by: 1.0/12.0/envelope scale do:
			[:val |
			y _ self yFromValue: val.
			graphArea addMorph: ((RectangleMorph
					newBounds: (r left@y extent: r width@1)
					color: Color veryLightGray)
						borderWidth: 0)]].
	(envelope updateSelector = #ratio: and: [denominator ~= 9999]) ifTrue:
		["Show denominator gridding"
		r _ graphArea innerBounds.
		(0.0 to: 1.0 by: 1.0/denominator/envelope scale) do:
			[:v |
			y _ self yFromValue: v.
			graphArea addMorph: ((RectangleMorph
					newBounds: (r left@y extent: r width@1)
					color: Color veryLightGray)
						borderWidth: 0)]].
