doTestDebuggerTempAccessWith: one with: two
	"Test debugger access for temps"
	| outerContext local1 remote1 |
	outerContext := thisContext.
	local1 := 3.
	remote1 := 1/2.
	self assert: (Compiler new evaluate: 'one' in: thisContext to: self) == one.
	self assert: (Compiler new evaluate: 'two' in: thisContext to: self) == two.
	self assert: (Compiler new evaluate: 'local1' in: thisContext to: self) == local1.
	self assert: (Compiler new evaluate: 'remote1' in: thisContext to: self) == remote1.
	Compiler new evaluate: 'local1 := -3.0' in: thisContext to: self.
	self assert: local1 = -3.0.
	(1 to: 2) do:
		[:i| | local2 r1 r2 r3 r4 |
		local2 := i * 3.
		remote1 := local2 / 7.
		self assert: thisContext ~~ outerContext.
		self assert: (r1 := Compiler new evaluate: 'one' in: thisContext to: self) == one.
		self assert: (r2 := Compiler new evaluate: 'two' in: thisContext to: self) == two.
		self assert: (r3 := Compiler new evaluate: 'i' in: thisContext to: self) == i.
		self assert: (r4 := Compiler new evaluate: 'local2' in: thisContext to: self) == local2.
		self assert: (r4 := Compiler new evaluate: 'remote1' in: thisContext to: self) == remote1.
		self assert: (r4 := Compiler new evaluate: 'remote1' in: outerContext to: self) == remote1.
		Compiler new evaluate: 'local2 := 15' in: thisContext to: self.
		self assert: local2 = 15.
		Compiler new evaluate: 'local1 := 25' in: thisContext to: self.
		self assert: local1 = 25.
		{ r1. r2. r3. r4 } "placate the compiler"].
	self assert: local1 = 25.
	self assert: remote1 = (6/7)