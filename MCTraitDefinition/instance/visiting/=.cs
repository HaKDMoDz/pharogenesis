= aDefinition
	self flag: #traits. "Ugly we harcoded the super superclass method.  We will have to refactor the definition hierarchy"
	
	^ (self isRevisionOf: aDefinition)
		and: [self traitCompositionString = aDefinition traitCompositionString]
		and: [category = aDefinition category]
		and: [comment = aDefinition comment]