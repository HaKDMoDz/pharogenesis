newForEncoding: aString 
	| class encoding |
	aString ifNil: [^ Latin1TextConverter new].
	encoding := aString asLowercase.
	class := self allSubclasses
				detect: [:each | each encodingNames includes: encoding]
				ifNone: [].
	class isNil
		ifTrue: [^ nil].
	^ class new