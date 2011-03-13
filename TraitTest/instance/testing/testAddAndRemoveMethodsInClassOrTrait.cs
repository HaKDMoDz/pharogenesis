testAddAndRemoveMethodsInClassOrTrait
	| aC2 |
	aC2 := self c2 new.
	self assert: aC2 m51.
	self c2 compile: 'm51 ^123'.
	self assert: aC2 m51 = 123.
	self c2 removeSelector: #m51.
	self shouldnt: [aC2 m51] raise: MessageNotUnderstood.
	self assert: aC2 m51.
	self t4 removeSelector: #m11.
	self assert: (self t4 methodDict includesKey: #m11)