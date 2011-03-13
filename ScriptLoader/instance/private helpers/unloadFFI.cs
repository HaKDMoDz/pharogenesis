unloadFFI
	"ScriptLoader new unloadFFI"
	
	| copies namesOfpackagesToUnload |
	namesOfpackagesToUnload := #('*FFI*'). 
	copies := MCWorkingCopy allManagers asSortedCollection:
		[ :a :b | a package name <= b package name ].
	(copies select: [:each | namesOfpackagesToUnload anySatisfy: [:ea | ea match: each package name ]])
		do: [:z | z unload].
	Smalltalk recreateSpecialObjectsArray