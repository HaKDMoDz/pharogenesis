privateParsePath: remainder relativeTo: basePath 
	| nextTok s parsedPath |
	s := ReadStream on: remainder.

	parsedPath := OrderedCollection new.
	parsedPath addAll: basePath.
	parsedPath isEmpty ifFalse: [ parsedPath removeLast ].
	
	[s peek = $/ ifTrue: [s next].
	nextTok := WriteStream on: String new.
	[s atEnd or: [s peek = $/]] whileFalse: [nextTok nextPut: s next].
	nextTok := nextTok contents unescapePercents.
	nextTok = '..' 
		ifTrue: [parsedPath size > 0 ifTrue: [parsedPath removeLast]]
		ifFalse: [nextTok ~= '.' ifTrue: [parsedPath add: nextTok]].
	s atEnd] 
			whileFalse.
	parsedPath isEmpty ifTrue: [parsedPath add: ''].

	^parsedPath