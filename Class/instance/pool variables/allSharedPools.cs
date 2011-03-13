allSharedPools
	"Answer a Set of the pools the receiver shares, including those defined  
	in the superclasses of the receiver."
	| aSet | 
	^self superclass == nil
		ifTrue: [self sharedPools copy]
		ifFalse: [aSet _ self superclass allSharedPools.
			aSet addAll: self sharedPools.
			aSet]