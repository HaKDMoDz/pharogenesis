buttonNormalBorderStyleFor: aButton
	"Return the normal button borderStyle for the given button."

	|c|
	c := aButton colorToUse darker.
	aButton isDefault
		ifTrue: [c := c alphaMixed: 0.5 with: Color black].
	^BorderStyle simple
		width: 1;
		baseColor: c