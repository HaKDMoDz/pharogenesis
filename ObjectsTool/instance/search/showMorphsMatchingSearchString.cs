showMorphsMatchingSearchString
	"Put items matching the search string into my lower pane"
	| quads |
	self setSearchStringFromSearchPane.
	self partsBin removeAllMorphs.
	Cursor wait
		showWhile: [quads := OrderedCollection new.
			Morph withAllSubclasses
				do: [:aClass | aClass
						addPartsDescriptorQuadsTo: quads
						if: [:info | info formalName translated includesSubstring: searchString caseSensitive: false]].
			self installQuads: quads fromButton: nil]