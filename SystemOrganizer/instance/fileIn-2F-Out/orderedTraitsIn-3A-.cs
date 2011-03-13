orderedTraitsIn: category 
	"Answer an OrderedCollection containing references to the traits in the 
	category whose name is the argument, category (a string). The traits 
	are ordered so they can be filed in."

	| behaviors traits |
	behaviors _ (self listAtCategoryNamed: category asSymbol) 
			collect: [:title | Smalltalk at: title].
	traits _ behaviors reject: [:each | each isBehavior].
	traits _ traits asSortedCollection: [:t1 :t2 |
		(t2 traitComposition allTraits includes: t1)
			or: [(t1 traitComposition allTraits includes: t2) not]].
	^traits asArray