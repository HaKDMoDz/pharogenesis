removeEmptyUnnamedChangeSets
	"Remove all change sets that are empty, whose names start with Unnamed,
		and which are not nailed down by belonging to a Project."
	"ChangeSorter removeEmptyUnnamedChangeSets"
	| toGo |
	(toGo _ (self changeSetsNamedSuchThat: [:csName | csName beginsWith: 'Unnamed'])
		select: [:cs | cs isEmpty and: [cs okayToRemoveInforming: false]])
		do: [:cs | AllChangeSets remove: cs wither].
	self inform: toGo size printString, ' change set(s) removed.'