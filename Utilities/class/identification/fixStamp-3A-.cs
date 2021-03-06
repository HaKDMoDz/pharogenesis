fixStamp: changeStamp 
	| parts |
	parts := changeStamp findTokens: ' '.
	(parts size > 0 and: [parts last first isLetter]) ifTrue:
		["Put initials first in all time stamps..."
		^ String streamContents:
				[:s | s nextPutAll: parts last.
				parts allButLast do: [:p | s space; nextPutAll: p]]].
	^ changeStamp