createActualMessageTo: aClass
	"Bundle up the selector, arguments and lookupClass into a Message object.
	In the process it pops the arguments off the stack, and pushes the message object.
	This can then be presented as the argument of eg, doesNotUnderstand:"

	| argumentArray message lookupClass |
	"remap lookupClass in case GC happens during allocation"
	self pushRemappableOop: aClass.
	argumentArray _ self instantiateClass: (self splObj: ClassArray)
							indexableSize: argumentCount.
	"remap argumentArray in case GC happens during allocation"
	self pushRemappableOop: argumentArray.
	message _ self instantiateClass: (self splObj: ClassMessage) indexableSize: 0.
	argumentArray _ self popRemappableOop.
	lookupClass _ self popRemappableOop.

	self beRootIfOld: argumentArray.
	self transfer: argumentCount
		from: stackPointer - ((argumentCount - 1) * 4)
		to: argumentArray + BaseHeaderSize.

	self storePointer: MessageSelectorIndex ofObject: message
		withValue: messageSelector.
	self storePointer: MessageArgumentsIndex ofObject: message
		withValue: argumentArray.
	(self lastPointerOf: message) >= (MessageLookupClassIndex*4 +BaseHeaderSize) ifTrue:
	["Only store lookupClass if message has 3 fields (old images don't)"
	self storePointer: MessageLookupClassIndex ofObject: message
		withValue: lookupClass].

	self pop: argumentCount thenPush: message.
	argumentCount _ 1.