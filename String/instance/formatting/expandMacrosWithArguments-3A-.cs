expandMacrosWithArguments: anArray 
	| newStream readStream char index |
	newStream := WriteStream on: (String new: self size).
	readStream := ReadStream on: self.
	[readStream atEnd] whileFalse: 
			[char := readStream next.
			char == $< 
				ifTrue: 
					[| nextChar |
					nextChar := readStream next asUppercase.
					nextChar == $N ifTrue: [newStream cr].
					nextChar == $T ifTrue: [newStream tab].
					nextChar isDigit 
						ifTrue: 
							[index := nextChar digitValue.
							
							[readStream atEnd 
								or: [(nextChar := readStream next asUppercase) isDigit not]] 
									whileFalse: [index := index * 10 + nextChar digitValue]].
					nextChar == $? 
						ifTrue: 
							[| trueString falseString |
							trueString := readStream upTo: $:.
							falseString := readStream upTo: $>.
							readStream position: readStream position - 1.
							newStream 
								nextPutAll: ((anArray at: index) ifTrue: [trueString] ifFalse: [falseString])].
					nextChar == $P 
						ifTrue: [newStream nextPutAll: (anArray at: index) printString].
					nextChar == $S ifTrue: [newStream nextPutAll: (anArray at: index)].
					readStream skipTo: $>]
				ifFalse: 
					[newStream nextPut: (char == $% ifTrue: [readStream next] ifFalse: [char])]].
	^newStream contents