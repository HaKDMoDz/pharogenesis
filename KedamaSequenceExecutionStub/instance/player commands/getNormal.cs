getNormal

	| headingRadians degrees |
	headingRadians _ (turtles arrays at: 7) at: self index.
	degrees _ 90.0 - headingRadians radiansToDegrees.
	^ degrees >= 0.0 ifTrue: [degrees] ifFalse: [degrees + 360.0].
