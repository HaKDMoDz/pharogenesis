initializeWithBaseTrait: aTrait
	self baseTrait: aTrait.
	self noteNewBaseTraitCompositionApplied: aTrait traitComposition.
	aTrait users do: [:each | self addUser: each classSide].
	