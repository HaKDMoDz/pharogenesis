emitStorePop: stack on: codeStream
	"This node has the form {expr storeAt: offset inTempFrame: homeContext},
	where the expr, the block argument, is already on the stack."
	^ self emitForEffect: stack on: codeStream