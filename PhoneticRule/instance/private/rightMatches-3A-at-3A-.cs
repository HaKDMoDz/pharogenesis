rightMatches: aString at: anInteger
	| rightindex textindex pattern |
	right isEmpty ifTrue: [^ true].
	rightindex _ 1.
	textindex _ anInteger + text size.
	[rightindex <= right size and: [textindex <= aString size]] whileTrue: [
		pattern _ right at: rightindex.
		"first check for simple text or apostrophe:"
		(pattern isAlphaNumeric or: [pattern = $'])
			ifTrue: [(aString at: textindex) asLowercase ~= pattern asLowercase
						ifTrue: [^ false].
					textindex _ textindex + 1].
		"space:"
		pattern = Character space
			ifTrue: [((aString at: textindex) isSeparator
						or: ['.,;:' includes: (aString at: textindex)]) ifFalse: [^ false].
					textindex _ textindex + 1].
		"one or more vowels:"
		pattern = $#
			ifTrue: [(aString at: textindex) isVowel ifFalse: [^ false].
					textindex _ textindex + 1.
					[textindex <= aString size and: [(aString at: textindex) isVowel]]
						whileTrue: [textindex _ textindex + 1]].
		"zero or more consonants:"
		pattern = $:
			ifTrue: [[textindex <= aString size
						and: ['bcdfghjklmnpqrstvwxyz'
								includes: (aString at: textindex) asLowercase]]
							whileTrue: [textindex _ textindex + 1]].
		"one consonant:"
		pattern = $^
			ifTrue: [('bcdfghjklmnpqrstvwxyz' includes: (aString at: textindex))
						ifFalse: [^ false].
					textindex _ textindex + 1].
		"b, d, v, g, j, l, m, n, r, w, z (voiced consonants):"
		pattern = $.
			ifTrue: [('bdvgjlmnrwz' includes: (aString at: textindex) asLowercase)
						ifFalse: [^ false].
					textindex _ textindex + 1].
		"e, i or y (front vowels):"
		pattern = $+
			ifTrue: [('eiy' includes: (aString at: textindex) asLowercase)
						ifFalse: [^ false].
					textindex _ textindex + 1].
		"er, e, es, ed, ing, ely (a suffix):"
		pattern = $%
			ifTrue: [(aString at: textindex) asLowercase = $e
						ifTrue: [textindex _ textindex + 1.
								(textindex < aString size and: [(aString at: textindex) asLowercase = $l])
									ifTrue: [textindex _ textindex + 1.
											(textindex < aString size and: [(aString at: textindex) asLowercase = $y])
												ifTrue: [textindex _ textindex + 1]
												ifFalse: [textindex _ textindex - 1]]
									ifFalse: [('rsd' includes: (aString at: textindex) asLowercase)
												ifTrue: [textindex _ textindex + 1]]]
						ifFalse: [(textindex + 2 <= aString size
									and: [(aString at: textindex) asLowercase = $i
										and: [(aString at: textindex + 1) asLowercase = $n
											and: [(aString at: textindex + 2) asLowercase = $g]]])
									ifTrue: [textindex _ textindex + 3]
									ifFalse: [^ false]]].
		rightindex _ rightindex + 1].
	^ true