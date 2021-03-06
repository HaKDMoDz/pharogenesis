testUsersWithClassChanges
	"This documents bug http://code.google.com/p/pharo/issues/detail?id=443"
	"self debug: #testUsersWithClassChanges"
	
	self c2 setTraitCompositionFrom: self t5.
	self assert: self t5 users size = 1.
	self assert: self t5 classSide users size = 1.
	self assert: self c2 traits size = 1.
	self assert: self c2 class traits size = 1.
	
	"Change class definition"
	self c2 addInstVarName: 'foo'.
	self assert: self t5 users size = 1.
	self assert: self t5 classSide users size = 1.
	self assert: self c2 traits size = 1.
	self assert: self c2 class traits size = 1.
	
	"Change metaclass definition"
	self c2 class instanceVariableNames: 'bar'.
	self assert: self t5 users size = 1.
	self assert: self t5 classSide users size = 1.
	self assert: self c2 traits size = 1.
	self assert: self c2 class traits size = 1.