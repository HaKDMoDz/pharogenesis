named: familyName
	"Answer the TextStyle with the given name, or nil."
	"TextStyle named: 'NewYork'"
	| textStyle |
	textStyle _ TextConstants at: familyName ifAbsent: [ ^nil ].
	(textStyle isKindOf: self) ifFalse: [ ^nil ].
	^textStyle