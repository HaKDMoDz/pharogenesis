addGraphic

	| graphic |
	graphic _ SketchMorph withForm: self speakerGraphic.
	graphic position: bounds center - (graphic extent // 2).
	self addMorph: graphic.
