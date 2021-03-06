checkboxButtonSelectedBorderStyleFor: aCheckboxButton
	"Return the normal checkbox button borderStyle for the given button."

	|c|
	c := self settings scrollbarColor alphaMixed: 0.7 with: Color black.
	^(CompositeBorder new width: 1)
		borders: {BorderStyle simple
					width: 1;
					baseColor: (Color black alpha: 0.1).
				BorderStyle simple
					width: 1;
					baseColor: c}