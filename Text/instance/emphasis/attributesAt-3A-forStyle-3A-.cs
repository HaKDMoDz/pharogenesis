attributesAt: characterIndex forStyle: aTextStyle
	"Answer the code for characters in the run beginning at characterIndex."
	| attributes |
	self size = 0
		ifTrue: [^ Array with: (TextFontChange new fontNumber: aTextStyle defaultFontIndex)].  "null text tolerates access"
	attributes _ runs at: characterIndex.
	^ attributes