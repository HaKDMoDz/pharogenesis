drawPolyPatchFrom: startPoint to: stopPoint on: aCanvas usingEnds: endsArray

	| cos sin tfm fill dir fsOrigin fsDirection points x y |
	dir _ (stopPoint - startPoint) normalized.
	"Compute the rotational transform from (0@0) -> (1@0) to startPoint -> stopPoint"
	cos _ dir dotProduct: (1@0).
	sin _ dir crossProduct: (1@0).
	"Now get the fill style appropriate for the given direction"
	fill _ self fillStyleForDirection: dir.
false ifTrue:[
	"Transform the fill appropriately"
	fill _ fill clone.
	"Note: Code below is inlined from tfm transformPoint:/transformDirection:"
	x _ fill origin x. y _ fill origin y.
	fsOrigin _ ((x * cos) + (y * sin) + startPoint x) @
					((y * cos) - (x * sin) + startPoint y).
	x _ fill direction x. y _ fill direction y.
	fsDirection _ ((x * cos) + (y * sin)) @ ((y * cos) - (x * sin)).
	fill origin: fsOrigin; 
		direction: fsDirection rounded; "NOTE: This is a bug in the balloon engine!!!"
		normal: nil.
	aCanvas asBalloonCanvas drawPolygon: endsArray fillStyle: fill.
] ifFalse:[
	"Transform the points rather than the fills"
	tfm _ (MatrixTransform2x3 new) a11: cos; a12: sin; a21: sin negated; a22: cos.
	"Install the start point offset"
	tfm offset: startPoint.
	points _ endsArray collect:[:pt| tfm invertPoint: pt].
	aCanvas asBalloonCanvas transformBy: tfm during:[:cc|
		cc drawPolygon: points fillStyle: fill.
	].
].