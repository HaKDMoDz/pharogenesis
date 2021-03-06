testMirrorInstVarAt
	| stackpBefore stackpAfter array point |
	stackpBefore := thisContext stackPtr.
	array := { 1. 2. 3 }.
	point := Point x: 1 y: 2.
	self assert: (thisContext object: array instVarAt: 1) = 1.
	self assert: (thisContext object: point instVarAt: 2) = 2.
	thisContext object: array instVarAt: 2 put: #two.
	self assert: array = #(1 #two 3).
	thisContext object: point instVarAt: 1 put: 1/2.
	self assert: point = (Point x: 1 / 2 y: 2).
	stackpAfter := thisContext stackPtr.
	self assert: stackpBefore = stackpAfter. "Make sure primitives pop all their arguments"
	self should: [thisContext object: array instVarAt: 4] raise: Error.
	self should: [thisContext object: point instVarAt: 3] raise: Error