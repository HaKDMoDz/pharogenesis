subMenuMarker
	"private - answer the form to be used as submenu marker"
	self isInDockingBar
		ifFalse: [^ self rightArrow].
	""
	owner isFloating
		ifTrue: [^ self bottomArrow].
	owner isAdheringToTop
		ifTrue: [^ self bottomArrow].
	owner isAdheringToBottom
		ifTrue: [^ self upArrow].
owner isAdheringToLeft ifTrue:[^ self rightArrow].
owner isAdheringToRight ifTrue:[^ self leftArrow].
	""
	^ self rightArrow