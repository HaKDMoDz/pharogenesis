explainClass: symbol 
	"Is symbol a class variable or a pool variable?"
	| class reply classes |
	(model respondsTo: #selectedClassOrMetaClass)
		ifFalse: [^ nil].
	(class _ model selectedClassOrMetaClass) ifNil: [^ nil].
	"no class is selected"
	(class isKindOf: Metaclass)
		ifTrue: [class _ class soleInstance].
	classes _ (Array with: class)
				, class allSuperclasses.
	"class variables"
	reply _ classes detect: [:each | (each classVarNames detect: [:name | symbol = name]
					ifNone: [])
					~~ nil]
				ifNone: [].
	reply == nil ifFalse: [^ '"is a class variable, defined in class ' , reply printString , '"\' withCRs , 'SystemNavigation new browseAllCallsOn: (' , reply printString , ' classPool associationAt: #' , symbol , ').'].
	"pool variables"
	classes do: [:each | (each sharedPools
			detect: [:pool | (pool includesKey: symbol)
					and: 
						[reply _ pool.
						true]]
			ifNone: [])
			~~ nil].
	reply
		ifNil: [(Undeclared includesKey: symbol)
				ifTrue: [^ '"is an undeclared variable.' , '"\' withCRs , 'SystemNavigation new browseAllCallsOn: (Undeclared associationAt: #' , symbol , ').']]
		ifNotNil: 
			[classes _ WriteStream on: Array new.
			self systemNavigation
				allBehaviorsDo: [:each | (each sharedPools
						detect: 
							[:pool | 
							pool == reply]
						ifNone: [])
						~~ nil ifTrue: [classes nextPut: each]].
			"Perhaps not print whole list of classes if too long. (unlikely)"
			^ '"is a pool variable from the pool ' , (Smalltalk keyAtIdentityValue: reply) asString , ', which is used by the following classes ' , classes contents printString , '"\' withCRs , 'SystemNavigation new browseAllCallsOn: (' , (Smalltalk keyAtIdentityValue: reply) asString , ' bindingOf: #' , symbol , ').'].
	^ nil