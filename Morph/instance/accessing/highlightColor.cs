highlightColor
	
	| val |
	^ (val _ self valueOfProperty: #highlightColor)
		ifNotNil:
			[val ifNil: [self error: 'nil highlightColor']]
		ifNil:
			[owner ifNil: [self color] ifNotNil: [owner highlightColor]]