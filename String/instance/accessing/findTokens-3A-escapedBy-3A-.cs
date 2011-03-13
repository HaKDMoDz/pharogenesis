findTokens: delimiters escapedBy: quoteDelimiters 
	"Answer a collection of Strings separated by the delimiters, where  
	delimiters is a Character or collection of characters. Two delimiters in a  
	row produce an empty string (compare this to #findTokens, which  
	treats sequential delimiters as one).  
	 
	The characters in quoteDelimiters are treated as quote characters, such  
	that any delimiter within a pair of matching quoteDelimiter characters  
	is treated literally, rather than as a delimiter.  
	 
	The quoteDelimiter characters may be escaped within a quoted string.  
	Two sequential quote characters within a quoted string are treated as  
	a single character.  
	 
	This method is useful for parsing comma separated variable strings for  
	spreadsheet import and export."

	| tokens rs activeEscapeCharacter ts char token delimiterChars quoteChars |
	delimiterChars _ (delimiters isNil
				ifTrue: ['']
				ifFalse: [delimiters]) asString.
	quoteChars _ (quoteDelimiters isNil
				ifTrue: ['']
				ifFalse: [quoteDelimiters]) asString.
	tokens _ OrderedCollection new.
	rs _ ReadStream on: self.
	activeEscapeCharacter _ nil.
	ts _ WriteStream on: ''.
	[rs atEnd]
		whileFalse: [char _ rs next.
			activeEscapeCharacter isNil
				ifTrue: [(quoteChars includes: char)
						ifTrue: [activeEscapeCharacter _ char]
						ifFalse: [(delimiterChars includes: char)
								ifTrue: [token _ ts contents.
									tokens add: token.
									ts _ WriteStream on: '']
								ifFalse: [ts nextPut: char]]]
				ifFalse: [char == activeEscapeCharacter
						ifTrue: [rs peek == activeEscapeCharacter
								ifTrue: [ts nextPut: rs next]
								ifFalse: [activeEscapeCharacter _ nil]]
						ifFalse: [ts nextPut: char]]].
	token _ ts contents.
	(tokens isEmpty and: [token isEmpty])
		ifFalse: [tokens add: token].
	^ tokens