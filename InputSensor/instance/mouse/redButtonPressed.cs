redButtonPressed
	"Answer true if only the red mouse button is being pressed.
	This is the first mouse button, usually the left one."

	^ (self primMouseButtons bitAnd: 7) = 4
