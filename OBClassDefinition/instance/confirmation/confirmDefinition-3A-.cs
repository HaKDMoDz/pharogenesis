confirmDefinition: definition
	"Check to make sure the user isn't accidentally over-writing an existing class."
	
	(((self isRedefinition: definition) not) and: [self definedClassExists: definition])
		ifTrue: [^ self confirmRedefinition: definition]
		ifFalse: [^ true]