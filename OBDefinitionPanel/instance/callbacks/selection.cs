selection
	^ self withDefinitionDo: [:def | def textSelection] ifNil: [1 to: 0]