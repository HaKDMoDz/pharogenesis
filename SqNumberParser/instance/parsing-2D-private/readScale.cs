readScale
	"read the scale if any (stored in instVar).
	Answer true if found, answer false if none.
	If scale letter is not followed by a digit,
	this is not considered as an error.
	Scales are always read in base 10, though i do not see why..."
	
	scale := 0.
	sourceStream atEnd ifTrue: [^ false].
	('s' includes: sourceStream next)
		ifFalse: [sourceStream skip: -1.
			^ false].
	scale := self
				nextUnsignedIntegerBase: 10
				ifFail: [sourceStream skip: -1.
					^ false].
	^ true