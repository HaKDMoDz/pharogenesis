internalBytecodeActivateNewMethod
	| methodHeader newContext tempCount argCount needsLarge |
	self inline: true.

	methodHeader _ self headerOf: newMethod.
	needsLarge _ methodHeader bitAnd: LargeContextBit.
	(needsLarge = 0 and: [freeContexts ~= NilContext])
		ifTrue: [newContext _ freeContexts.
				freeContexts _ self fetchPointer: 0 ofObject: newContext]
		ifFalse: ["Slower call for large contexts or empty free list"
				self externalizeIPandSP.
				newContext _ self allocateOrRecycleContext: needsLarge.
				self internalizeIPandSP].
	tempCount _ (methodHeader >> 19) bitAnd: 16r3F.

	"Assume: newContext will be recorded as a root if necessary by the
	 call to newActiveContext: below, so we can use unchecked stores."

	self storePointerUnchecked: SenderIndex	ofObject: newContext
		withValue: activeContext.
	self storeWord: InstructionPointerIndex	ofObject: newContext
		withValue: (self integerObjectOf:
			(((LiteralStart + (self literalCountOfHeader: methodHeader)) * 4) + 1)).
	self storeWord: StackPointerIndex			ofObject: newContext
		withValue: (self integerObjectOf: tempCount).
	self storePointerUnchecked: MethodIndex ofObject: newContext
		withValue: newMethod.

	"Copy the reciever and arguments..."
	argCount _ argumentCount.
	0 to: argCount do:
		[:i | self storePointerUnchecked: ReceiverIndex+i ofObject: newContext
			withValue: (self internalStackValue: argCount-i)].

	"clear remaining temps to nil in case it has been recycled"
	methodHeader _ nilObj.  "methodHeader here used just as faster (register?) temp"
	argCount+1 to: tempCount do:
		[:i | self storePointerUnchecked: ReceiverIndex+i ofObject: newContext
			withValue: methodHeader].

	self internalPop: argCount + 1.
	reclaimableContextCount _ reclaimableContextCount + 1.
	self internalNewActiveContext: newContext.
