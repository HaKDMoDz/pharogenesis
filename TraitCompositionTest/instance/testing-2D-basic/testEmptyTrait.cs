testEmptyTrait
	| composition |
	composition _ {} asTraitComposition.
	
	self assert: (composition isKindOf: TraitComposition).
	self assert: composition transformations isEmpty.
	self assert: composition traits isEmpty