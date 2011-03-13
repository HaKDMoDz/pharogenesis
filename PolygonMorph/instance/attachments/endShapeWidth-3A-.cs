endShapeWidth: aWidth
	| originalWidth originalVertices transform |
	originalWidth _ self valueOfProperty: #originalWidth ifAbsentPut: [ self borderWidth isZero ifFalse: [ self borderWidth ] ifTrue: [ 2 ] ].
	self borderWidth: aWidth.
	originalVertices _ self valueOfProperty: #originalVertices ifAbsentPut: [
		self vertices collect: [ :ea | (ea - (self referencePosition)) rotateBy: self heading degreesToRadians about: 0@0 ]
	].
	transform _ MorphicTransform offset: 0@0 angle: self heading degreesToRadians scale: originalWidth / aWidth.
	self setVertices: (originalVertices collect: [ :ea |
		((transform transform: ea) + self referencePosition) asIntegerPoint
	]).
	self computeBounds.