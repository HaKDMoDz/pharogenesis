inspectInstances
	"Inspect all instances of the selected class."

	| myClass |
	(myClass := self selectedClassOrMetaClass) ifNotNil:
		[myClass theNonMetaClass inspectAllInstances]. 
