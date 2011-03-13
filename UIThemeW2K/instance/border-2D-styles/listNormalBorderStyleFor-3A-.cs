listNormalBorderStyleFor: aList
	"Return the normal borderStyle for the given list"

	| aStyle |
	aStyle := BorderStyle complexInset.
	aStyle width: 2.
	aStyle color: self backgroundColor.
	aStyle baseColor: self backgroundColor.
	^aStyle