addInstVarName: aString
	"Add the argument, aString, as one of the receiver's instance variables."
	^(ClassBuilder new)
		name: self name
		inEnvironment: self environment
		subclassOf: superclass
		type: self typeOfClass
		instanceVariableNames: self instanceVariablesString , aString
		classVariableNames: self classVariablesString
		poolDictionaries: self sharedPoolsString
		category: self category
