recordDeclarations
	"Record C type declarations of the forms

		self returnTypeC: 'float'.
		self var: #foo declareC: 'float foo'
		self var: #foo as: Class
		self var: #foo type: 'float'.

	 and remove the declarations from the method body."

	| newStatements isDeclaration theClass varName varType |
	newStatements _ OrderedCollection new: parseTree statements size.
	parseTree statements do: 
		[:stmt |
		 isDeclaration _ false.
		 stmt isSend ifTrue: 
			[stmt selector = #var:declareC: ifTrue:
				[isDeclaration _ true.
				declarations at: stmt args first value asString put: stmt args last value].
			stmt selector = #var:type: ifTrue: [
				isDeclaration _ true.
				varName _ stmt args first value asString.
				varType _ stmt args last value.
				declarations at: varName put: (varType, ' ', varName).
			].
			 stmt selector = #var:as: ifTrue:
				[isDeclaration _ true.
				 theClass _ Smalltalk 
					at: stmt args last name asSymbol
					ifAbsent: [^self error: 'declarator must be a Behavior'].
				 (theClass isKindOf: Behavior)
					ifFalse: [^self error: 'declarator must be a Behavior'].
				 declarations 
					at: stmt args first value asString 
					put: (theClass ccgDeclareCForVar: stmt args first value asString)].
			 stmt selector = #returnTypeC: ifTrue: 
				[isDeclaration _ true.
				 returnType _ stmt args last value]].
		 isDeclaration ifFalse: [newStatements add: stmt]].
	parseTree setStatements: newStatements asArray