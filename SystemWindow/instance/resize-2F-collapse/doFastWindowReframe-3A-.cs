doFastWindowReframe: ptName

	| newBounds |
	"For fast display, only higlight the rectangle during loop"
	newBounds _ self bounds newRectButtonPressedDo: [:f | 
		f 
			withSideOrCorner: ptName
			setToPoint: (self pointFromWorld: Sensor cursorPoint)
			minExtent: self minimumExtent].
	self bounds: newBounds.
	^newBounds.