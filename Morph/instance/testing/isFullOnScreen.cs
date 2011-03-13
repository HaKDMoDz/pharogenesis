isFullOnScreen
	"Answer if the receiver is full contained in the owner visible  
	area."
	owner isInMemory
		ifFalse: [^ true].
	owner isNil
		ifTrue: [^ true].
	self visible
		ifFalse: [^ true].
	^ owner clearArea containsRect: self fullBounds