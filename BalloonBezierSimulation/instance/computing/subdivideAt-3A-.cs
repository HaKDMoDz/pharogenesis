subdivideAt: parameter
	"Subdivide the receiver at the given parameter"
	| both |
	(parameter <= 0.0 or:[parameter >= 1.0]) ifTrue:[self halt].
	both := self computeSplitAt: parameter.
	"Transcript cr.
	self quickPrint: self.
	Transcript space.
	self quickPrint: both first.
	Transcript space.
	self quickPrint: both last.
	Transcript endEntry."
	self via: both first via.
	self end: both first end.
	^both last