asStringWithCr
	"Convert to a string with returns between items.  Elements are
usually strings.
	 Useful for labels for PopUpMenus."
	| labelStream |
	labelStream _ WriteStream on: (String new: 200).
	self do: [:each |
		(each isKindOf: AbstractString)
			ifTrue: [labelStream nextPutAll: each; cr]
			ifFalse: [each printOn: labelStream. labelStream cr]].
	self size > 0 ifTrue: [labelStream skip: -1].
	^ labelStream contents