mouseDownInDimissHandle: evt with: dismissHandle
	evt hand obtainHalo: self.
	Preferences soundsEnabled ifTrue: [TrashCanMorph playMouseEnterSound].
	self removeAllHandlesBut: dismissHandle.
	self setColor: Color darkGray toHandle: dismissHandle.
