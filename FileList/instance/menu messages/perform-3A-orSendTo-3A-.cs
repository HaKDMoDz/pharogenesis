perform: selector orSendTo: otherTarget 
	"Selector was just chosen from a menu by a user.
	If it's one of the three sort-by items, handle it specially.
	If I can respond myself, then perform it on myself. 
	If not, send it to otherTarget, presumably the editPane from which the menu was invoked."

	^ (#(sortByDate sortBySize sortByName) includes: selector)
		ifTrue:
			[self resort: selector]
		ifFalse:
			[(#(get getHex copyName openImageInWindow importImage renameFile deleteFile addNewFile) includes: selector)
				ifTrue: [self perform: selector]
				ifFalse: [super perform: selector orSendTo: otherTarget]]