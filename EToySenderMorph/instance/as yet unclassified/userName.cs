userName

	^ (self 
		findDeepSubmorphThat: [ :x | x isKindOf: StringMorph] 
		ifAbsent: [^nil]) contents
