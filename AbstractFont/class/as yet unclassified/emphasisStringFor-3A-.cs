emphasisStringFor: emphasisCode
	"Answer a translated string that represents the attributes given in emphasisCode."

	| emphases bit |
	emphasisCode = 0 ifTrue: [ ^'Normal' translated ].

	emphases := (IdentityDictionary new)
		at: 1 put: 'Bold' translated;
		at: 2 put: 'Italic' translated;
		at: 4 put: 'Underlined' translated;
		at: 8 put: 'Narrow' translated;
		at: 16 put: 'StruckOut' translated;
		yourself.

	bit := 1.
	^String streamContents: [ :s |
		[ bit < 32 ] whileTrue: [ | code |
			code := emphasisCode bitAnd: bit.
			code isZero ifFalse: [ s nextPutAll: (emphases at: code); space ].
			bit := bit bitShift: 1 ].
		s position isZero ifFalse: [ s skip: -1 ].
	]