anyButtonPressed
	"Answer whether at least one mouse button is currently being pressed."

	^ self primMouseButtons anyMask: 7
