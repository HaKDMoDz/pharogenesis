blankIconOfWidth: aNumber 
	^ Icons
		at: ('blankIcon-' , aNumber asString) asSymbol
		ifAbsentPut: [Form extent: aNumber @ 1 depth:8]