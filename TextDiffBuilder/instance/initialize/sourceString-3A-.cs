sourceString: aString
	realSrc := self split: aString asString.
	srcLines := OrderedCollection new.
	srcMap := OrderedCollection new.
	realSrc doWithIndex:[:line :realIndex|
		"(line contains:[:anyChar| anyChar isSeparator not]) ifTrue:["
			srcLines add: line.
			srcMap add: realIndex.
		"]."
	].
	srcPos := PluggableDictionary new: srcLines size.
	srcPos hashBlock: self stringHashBlock.
	srcLines doWithIndex:[:line :index| 
		(srcPos includesKey: line)
			ifTrue:[(srcPos at: line) add: index. multipleMatches := true]
			ifFalse:[srcPos at: line put: (OrderedCollection with: index)]].