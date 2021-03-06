testMirrorAt
	| stackpBefore stackpAfter array byteArray |
	stackpBefore := thisContext stackPtr.
	array := { 1. 2. 3 }.
	byteArray := ByteArray with: 1 with: 2 with: 3.
	self assert: (thisContext object: array basicAt: 1) = 1.
	self assert: (thisContext object: byteArray basicAt: 2) = 2.
	thisContext object: array basicAt: 2 put: #two.
	self assert: array = #(1 #two 3).
	thisContext object: byteArray basicAt: 2 put: 222.
	self assert: byteArray asArray = #(1 222 3).
	stackpAfter := thisContext stackPtr.
	self assert: stackpBefore = stackpAfter. "Make sure primitives pop all their arguments"
	self should: [thisContext object: array basicAt: 4] raise: Error.
	self should: [thisContext object: byteArray basicAt: 0] raise: Error.
	self should: [thisContext object: byteArray basicAt: 1 put: -1] raise: Error