closedCubicSlopes: clampedSlopes
	"Sent to knots returns a copy of clampedSlopes with the values of the undefined (nil)  slopes filled in.
	"
	" clampedSlopes must be the same size as knots)" 
	
	"/* Between known slopes we solve the equation for knots with end conditions:  
	4*b1+b2 = 3(a2 - a0) - b0 
	bN2+4*bN1 = 3*(aN-aN2) - bN
	and inbetween:
	b2+4*b3+b4=3*(a4-a2)
	where a2 is (knots atWrap: index + 1) etc.
	and the b's are the slopes .
	N is the last index (knots size)
	N1 is N-1.
	 
	by using row operations to convert the matrix to upper  
	triangular and then back substitution. 
	"
	| slopes tripleKnots list |
	(list := clampedSlopes closedFillinList) = { 0 to: self size } ifTrue: [ ^ self closedCubicSlopes ] .
	"Special case all unknown."
	
	tripleKnots := self * 3.0 . 
	" Premultiply and convert numbers or point coords to Floats "
	slopes := clampedSlopes copy. "slopes contents will be modified."
	
	list do: [ :r | slopes slopesWith: tripleKnots from: r first to: r last ] .
	
	^ slopes