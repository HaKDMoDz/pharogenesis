ifEmpty: aBlock

	self hasSubmorphs not ifTrue: [^aBlock value]