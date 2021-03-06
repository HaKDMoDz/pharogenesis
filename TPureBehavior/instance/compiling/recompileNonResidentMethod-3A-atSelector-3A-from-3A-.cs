recompileNonResidentMethod: method atSelector: selector from: oldClass
	"Recompile the method supplied in the context of this class."

	| trailer methodNode |
	trailer := method trailer.
	methodNode := self compilerClass new
			compile: (method getSourceFor: selector in: oldClass)
			in: self
			notifying: nil
			ifFail: ["We're in deep doo-doo if this fails (syntax error).
				Presumably the user will correct something and proceed,
				thus installing the result in this methodDict.  We must
				retrieve that new method, and restore the original (or remove)
				and then return the method we retrieved."
				^ self error: 'see comment'].
	selector == methodNode selector ifFalse: [self error: 'selector changed!'].
	^ methodNode generate: trailer
