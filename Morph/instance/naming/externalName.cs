externalName
	^ self knownName ifNil: [self innocuousName]