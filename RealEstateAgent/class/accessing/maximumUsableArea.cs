maximumUsableArea

	| allowedArea |
	allowedArea := Display usableArea.
	allowedArea := allowedArea intersect: ActiveWorld visibleClearArea.
	^allowedArea