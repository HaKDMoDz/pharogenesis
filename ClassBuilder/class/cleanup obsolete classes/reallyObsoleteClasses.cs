reallyObsoleteClasses
	| obsoleteClasses |
	obsoleteClasses _ OrderedCollection new.
	Metaclass allInstances do: [:meta | meta allInstances do: [:each | 
		(self isReallyObsolete: each) ifTrue: [obsoleteClasses add: each]]].
	^ obsoleteClasses