shouldNotBeRedefined
	"Return true if the receiver should not be redefined.
	The assumption is that compact classes,
	classes in Smalltalk specialObjects and 
	Behaviors should not be redefined"
	^(Smalltalk compactClassesArray includes: self)
		or:[(Smalltalk specialObjectsArray includes: self)
			or:[self isKindOf: self]]