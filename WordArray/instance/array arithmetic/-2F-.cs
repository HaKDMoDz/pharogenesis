/ other

	| result |
	other isNumber ifTrue: [
		other isFloat ifTrue: [
			result := KedamaFloatArray new: self size.
			^ self primDivScalar: self and: other into: result.
		] ifFalse: [
			result := WordArray new: self size.
			^ self primDivScalar: self and: other into: result.
		].
	].
	(other isMemberOf: WordArray) ifTrue: [	
		result := WordArray new: self size.
		^ self primDivArray: self and: other into: result.
	].
	(other isMemberOf: KedamaFloatArray) ifTrue: [	
		result := KedamaFloatArray new: self size.
		^ self primDivArray: self and: other into: result.
	].
	^ super / other.
