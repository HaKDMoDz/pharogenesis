allSendersOf: selector inClassCategory: category 
	| classes sortedSenders special byte |
	classes := SystemOrganization classesInCategory: category.
	sortedSenders := SortedCollection new.
	classes ifEmpty: [ ^ sortedSenders ].
	special := classes anyOne environment 
		hasSpecialSelector: selector
		ifTrueSetByte: [ :b | byte := b ].
	classes do: 
		[ :class | 
		self 
			addSelectorsReferingTo: selector
			in: class
			to: sortedSenders
			special: special
			byte: byte ].
	^ sortedSenders