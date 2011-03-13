setScrollDeltas
	| range interval value |
	transform hasSubmorphs ifFalse: [scrollBar interval: 1.0. ^ self].
	range _ self leftoverScrollRange.
	range = 0 ifTrue: [^ scrollBar interval: 1.0; setValue: 0].
	interval _ ((self innerBounds width) / self totalScrollRange) asFloat.
	value _ (transform offset x / range min: 1.0) asFloat.
	scrollBar interval: interval.
	scrollBar setValue: value.