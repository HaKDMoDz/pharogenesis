handleColor

	^ handleColor ifNil: [self setDefaultColors. handleColor]