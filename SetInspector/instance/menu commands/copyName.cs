copyName
	"Copy the name of the current variable, so the user can paste it into the 
	window below and work with is. If collection, do (xxx at: 1)."
	| sel |
	self selectionIndex <= (2 + object class instSize)
		ifTrue: [super copyName]
		ifFalse: [sel _ '(self array at: '
						, (String streamContents: 
							[:strm | self arrayIndexForSelection storeOn: strm]) , ')'.
			Clipboard clipboardText: sel asText]