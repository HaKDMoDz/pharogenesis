gatherChangeSets		"ChangeSet gatherChangeSets"
	"Collect any change sets created in other projects"
	| allChangeSets obsolete |
	allChangeSets := AllChangeSets asSet.
	ChangeSet allSubInstances do: [:each |
		(allChangeSets includes: each) == (obsolete := each isMoribund) ifTrue:[
			obsolete
				ifTrue: ["Was included and is obsolete."
						AllChangeSets remove: each]
				ifFalse: ["Was not included and is not obsolete."
						AllChangeSets add: each]]].
	^ AllChangeSets