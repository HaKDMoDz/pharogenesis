initializeForPropertiesPanel
	"Initialize the receiver.  If beModal is true, it will be a modal color picker, else not"

	isModal _ false.
	self removeAllMorphs.
	self setProperty: #noDraggingThisPicker toValue: true.

	self addMorph: ((Morph newBounds: (RevertBox translateBy: self topLeft))
			color: Color transparent; setCenteredBalloonText: 'restore original color' translated).
	self addMorph: ((Morph newBounds: (FeedbackBox translateBy: self topLeft))
			color: Color transparent; setCenteredBalloonText: 'shows selected color' translated).
	self addMorph: ((Morph newBounds: (TransparentBox translateBy: self topLeft))
			color: Color transparent; setCenteredBalloonText: 'adjust translucency' translated).

	self buildChartForm.
	
	selectedColor ifNil: [selectedColor _ Color white].
	sourceHand _ nil.
	deleteOnMouseUp _ false.
	updateContinuously _ true.
