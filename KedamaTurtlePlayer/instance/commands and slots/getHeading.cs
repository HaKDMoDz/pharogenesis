getHeading
	"Answer my heading in degrees."

	| degrees |
	degrees _ 90.0 - headingRadians radiansToDegrees.
	^ degrees >= 0.0 ifTrue: [degrees] ifFalse: [degrees + 360.0].
