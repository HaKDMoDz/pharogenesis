methods
	^ MCMockClassA selectors
		select: [:ea | ea beginsWith: 'ordinal']
		thenCollect:
			[:ea | 
				MethodReference new 
					setStandardClass: MCMockClassA 
					methodSymbol: ea].