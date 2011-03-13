methodDescriptionsForSelector: aSymbol
	"Return a collection of TraitMethodDescriptions for aSymbol and all the 
	aliases of aSymbol."

	| selectors collection |
	selectors _ IdentitySet with: aSymbol.
	self transformations do: [:each |
		selectors addAll: (each aliasesForSelector: aSymbol)].
	collection _ OrderedCollection new: selectors size.
	selectors do: [:each |
		collection add: (self methodDescriptionForSelector: each)].
	^collection