closeAllViews
	"self new closeAllViews"

	self dependents do: 
			[:d | (d isSystemWindow) ifTrue: [d delete]]