unbindExternalPrimitives
	"Primitive. Force all external primitives to be looked up again afterwards. Since external primitives that have not found are bound for fast failure this method will force the lookup of all primitives again so that after adding some plugin the primitives may be found."
	^ self deprecated: 'Use SmalltalkImage unbindExternalPrimitives'
		block: [SmalltalkImage unbindExternalPrimitives].
	"Do nothing if the primitive fails for compatibility with older VMs"