showListWithAuthors: aCollection prefix: prefix
	"Return an HTML list to reference an ordered collection of notes. prefix.index will be the reference URL for the note. Special version for linking to author's homepage, if any."
	| ret aNote |
	ret _ WriteStream on: String new.
	ret nextPutAll: '<ul>' ; cr.
	1 to: aCollection size do: [:each |
		ret nextPutAll: '<li>'.
		aNote := aCollection at: each.
		ret nextPutAll: (self linkTo: (prefix , each printString) label: (aNote title)),'-'.
		ret nextPutAll: (self linkTo: (aNote author homepage) label: aNote author name)
			,'-',aNote timestamp ; cr.
		aNote children size > 0 ifTrue: 
			[ret nextPutAll: (self showList: aNote children prefix: (prefix , each printString))].].
	ret nextPutAll: '</ul>'.
	^ret contents
