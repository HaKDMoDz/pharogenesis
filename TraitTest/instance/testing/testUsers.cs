testUsers
	self assert: self t1 users size = 3.
	self assert: (self t1 users includesAllOf: {self t4. self t5. self t6 }).
	self assert: self t3 users isEmpty.
	self assert: self t5 users size = 1.
	self assert: self t5 users anyOne = self c2.
	self c2 setTraitCompositionFrom: self t1 + self t5.
	self assert: self t5 users size = 1.
	self assert: self t5 users anyOne = self c2.
	self c2 setTraitComposition: self t2 asTraitComposition.
	self assert: self t5 users isEmpty