messageIn: aThemedMorph text: aStringOrText title: aString
	"Answer the result of a message dialog (true) with the given label and title."

	self messageSound play.
	^(aThemedMorph openModal: (
		MessageDialogWindow new
			textFont: self textFont;
			title: aString;
			text: aStringOrText)) cancelled not