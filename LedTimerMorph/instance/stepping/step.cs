step

	flash
		ifTrue: [super step]
		ifFalse: [
			counting ifTrue: [self updateTime]]