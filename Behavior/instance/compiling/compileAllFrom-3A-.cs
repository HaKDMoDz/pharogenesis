compileAllFrom: oldClass
	"Compile all the methods in the receiver's method dictionary.
	This validates sourceCode and variable references and forces
	all methods to use the current bytecode set"
	"ar 7/10/1999: Use oldClass selectors not self selectors"
	oldClass selectorsDo: [:sel | self recompile: sel from: oldClass].
	self environment currentProjectDo: [:proj | proj compileAllIsolated: self from: oldClass].