doPrimitive: primitiveName 
	| proxy plugin |
	proxy _ InterpreterProxy new.
	proxy loadStackFrom: thisContext sender.
	plugin _ self simulatorClass new.
	plugin setInterpreter: proxy.
	plugin perform: primitiveName asSymbol.
	^ proxy stackValue: 0