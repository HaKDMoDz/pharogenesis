testAliasCompositions
	"unary"

	self 
		shouldnt: [self t2 setTraitCompositionFrom: self t1 @ { (#aliasM11 -> #m11) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#alias: -> #m11) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#alias:x:y: -> #m11) }]
		raise: TraitCompositionException.

	"binary"
	self t1 compile: '= anObject'.
	self 
		shouldnt: [self t2 setTraitCompositionFrom: self t1 @ { (#equals: -> #=) }]
		raise: TraitCompositionException.
	self shouldnt: [self t2 setTraitCompositionFrom: self t1 @ { (#% -> #=) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#equals -> #=) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#equals:x: -> #=) }]
		raise: TraitCompositionException.

	"keyword"
	self t1 compile: 'x: a y: b z: c'.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#'==' -> #x:y:z:) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#x -> #x:y:z:) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#x: -> #x:y:z:) }]
		raise: TraitCompositionException.
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#x:y: -> #x:y:z:) }]
		raise: TraitCompositionException.
	self shouldnt: 
			[self t2 setTraitCompositionFrom: self t1 @ { (#myX:y:z: -> #x:y:z:) }]
		raise: TraitCompositionException.

	"alias same as selector"
	self 
		should: [self t2 setTraitCompositionFrom: self t1 @ { (#m11 -> #m11) }]
		raise: TraitCompositionException.

	"same alias name used twice"
	self should: 
			[self t2 
				setTraitCompositionFrom: self t1 @ { (#alias -> #m11). (#alias -> #m12) }]
		raise: TraitCompositionException.

	"aliasing an alias"
	self should: 
			[self t2 
				setTraitCompositionFrom: self t1 @ { (#alias -> #m11). (#alias2 -> #alias) }]
		raise: TraitCompositionException