resetClickState
	"Reset the double-click detection state to normal (i.e., not waiting for a double-click)."

	clickClient _ nil.
	clickState _ #idle.
	eventTransform _ MorphicTransform identity.
	firstClickEvent _ nil.
	firstClickTime _ nil.
