unloadModule: aString
	"Primitive. Unload the given module.
	This primitive is intended for development only since some
	platform do not implement unloading of DLL's accordingly.
	Also, the mechanism for unloading may not be supported
	on all platforms."
	^self deprecated: 'Use SmalltalkImage current unloadModule:'
		block: [ SmalltalkImage current unloadModule: aString]