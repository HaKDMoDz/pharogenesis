at: index put: aCharacter
	"Primitive. Store the Character in the field of the receiver indicated by
	the index. Fail if the index is not an Integer or is out of bounds, or if
	the argument is not a Character. Essential. See Object documentation
	whatIsAPrimitive."

	<primitive: 64>
	(aCharacter isMemberOf: Character) ifTrue: [	
		index isInteger
			ifTrue: [self errorSubscriptBounds: index]
			ifFalse: [self errorNonIntegerIndex]
	] ifFalse: [
		(aCharacter isMemberOf: MultiCharacter) 
			ifTrue: [
				self becomeForward: (MultiString from: self).
				self at: index put: aCharacter.
			] ifFalse: [	
				self error: 'Strings only store Characters'
			]
	].
