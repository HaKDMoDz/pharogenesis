opPrimMultiply

	| rcvr arg result |
	(self initOp: PrimMultiply) ifFalse: [
	self beginOp: PrimMultiply.

		rcvr _ self internalStackValue: 1.
		arg _ self internalStackValue: 0.
		(self areIntegers: rcvr and: arg) ifTrue: [
			rcvr _ self integerValueOf: rcvr.
			arg _ self integerValueOf: arg.
			result _ rcvr * arg.
			((arg = 0 or: [(result // arg) = rcvr]) and:
			 [self isIntegerValue: result]) ifTrue: [
				self longAt: (localSP _ localSP - 4)
						put: (self integerObjectOf: result).
				self skip: 1.
				^self nextOp.
				].
		].

		self skip: 1.
		self externalizeIPandSP.
		successFlag _ true.
		self primitiveFloatMultiply.
		self internalizeIPandSP.
		successFlag ifFalse: [self sendSpecialSelector: 8 nArgs: 1].

	self endOp: PrimMultiply
	]