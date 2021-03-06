backWord: characterStream 
	"If the selection is not a caret, delete it and leave it in the backspace buffer.
	 Else if there is typeahead, delete it.
	 Else, delete the word before the caret."

	| startIndex |
	characterStream isEmpty
		ifTrue:
			[self hasCaret
				ifTrue: "a caret, delete at least one character"
					[startIndex := 1 max: self markIndex - 1.
					[startIndex > 1 and:
						[(paragraph text at: startIndex - 1) asCharacter tokenish]]
						whileTrue:
							[startIndex := startIndex - 1]]
				ifFalse: "a non-caret, just delete it"
					[startIndex := self markIndex].
			self backTo: startIndex]
		ifFalse:
			[characterStream reset].
	^false