browseAllAccessesTo: instVarName from: aClass
	"Create and schedule a Message Set browser for all the receiver's methods 
	or any methods of a subclass/superclass that refer to the instance variable name."

	"self new browseAllAccessesTo: 'contents' from: Collection."
	
	| coll |
	coll _ OrderedCollection new.
	Cursor wait showWhile: [
		aClass withAllSubAndSuperclassesDo: [:class | 
			(class whichSelectorsAccess: instVarName) do: [:sel |
				sel == #DoIt ifFalse: [
					coll add: (
						MethodReference new
							setStandardClass: class 
							methodSymbol: sel
					)
				]
			]
		].
	].
	^ self 
		browseMessageList: coll 
		name: 'Accesses to ' , instVarName 
		autoSelect: instVarName