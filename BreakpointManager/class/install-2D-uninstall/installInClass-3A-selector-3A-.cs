installInClass: aClass selector: aSymbol 
	"Install a new method containing a breakpoint.
	The receiver will remember this for unstalling it later"

	| breakMethod |
	breakMethod _ self compilePrototype: aSymbol in: aClass.
	breakMethod isNil
		ifTrue: [^ nil].
	self installed at: breakMethod put: aClass >> aSymbol. "old method"
	aClass methodDictionary at: aSymbol put: breakMethod.