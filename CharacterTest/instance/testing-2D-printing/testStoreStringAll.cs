testStoreStringAll
	Character allCharacters do: [ :each |
		self assert: (self class compilerClass 
			evaluate: each storeString) = each ].