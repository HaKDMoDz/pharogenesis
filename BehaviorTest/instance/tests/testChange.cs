testChange
	"self debug: #testChange"

	| behavior model |
	behavior := Behavior new.
	behavior superclass: Model.
	behavior setFormat: Model format.
	model := Model new.
	model primitiveChangeClassTo: behavior new.
	behavior compile: 'thisIsATest  ^ 2'.
	self assert: model thisIsATest = 2.
	self should: [Model new thisIsATest] raise: MessageNotUnderstood.


