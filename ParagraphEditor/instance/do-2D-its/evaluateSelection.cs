evaluateSelection
	"Treat the current selection as an expression; evaluate it and return the result"
	| result rcvr ctxt |
	self lineSelectAndEmptyCheck: [^ ''].

	(model respondsTo: #doItReceiver) 
		ifTrue: [FakeClassPool adopt: model selectedClass.  "Include model pool vars if any"
				rcvr _ model doItReceiver.
				ctxt _ model doItContext]
		ifFalse: [rcvr _ ctxt _ nil].
	result _ [
		rcvr class evaluatorClass new 
			evaluate: self selectionAsStream
			in: ctxt
			to: rcvr
			notifying: self
			ifFail: [FakeClassPool adopt: nil. ^ #failedDoit]
			logged: true.
	] 
		on: OutOfScopeNotification 
		do: [ :ex | ex resume: true].
	FakeClassPool adopt: nil.
	^ result