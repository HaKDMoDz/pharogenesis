measureFor: n 
	"Return a distance measure for cubic curve with n segments. 
	For convienence and accuracy we use the sum of the
	distances. "
	"first point is poly of 0."
	| p1 p2 measure |
	p1 := self first.
	measure := 0.
	(1 to: n)
		do: [:i | 
			p2 := self polynomialEval: i / n asFloat.
			measure := measure
						+ (p2 dist: p1).
			p1 := p2].
	^ measure