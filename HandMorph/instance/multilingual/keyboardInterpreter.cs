keyboardInterpreter

	^keyboardInterpreter ifNil: [keyboardInterpreter _ LanguageEnvironment currentPlatform class defaultInputInterpreter]