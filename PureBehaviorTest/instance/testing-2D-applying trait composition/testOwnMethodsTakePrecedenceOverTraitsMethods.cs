testOwnMethodsTakePrecedenceOverTraitsMethods
	"First create a trait with no subtraits and then
	add subtrait t1 which implements m11 as well."

	| trait |
	trait := self createTraitNamed: #T
				uses: { }.
	trait compile: 'm11 ^999'.
	self assert: trait methodDict size = 1.
	self assert: (trait methodDict at: #m11) decompileString = 'm11
	^ 999'.
	Trait 
		named: #T
		uses: self t1
		category: self class category.
	self assert: trait methodDict size = 3.
	self assert: (trait methodDict keys includesAllOf: #(#m11 #m12 #m13 )).
	self assert: (trait methodDict at: #m11) decompileString = 'm11
	^ 999'.
	self assert: (trait methodDict at: #m12) decompileString = 'm12
	^ 12'