methodRefList
	"Return a MethodReference for each message I can send. tk 9/13/97, raa 
	5/29/01 "
	| list adder |
	list _ SortedCollection new.
	adder _ [:recip :sel | recip
				ifNotNil: [list
						add: (MethodReference new
								setStandardClass: (recip class whichClassIncludesSelector: sel)
								methodSymbol: sel)]].
	adder value: mouseDownRecipient value: mouseDownSelector.
	adder value: mouseMoveRecipient value: mouseMoveSelector.
	adder value: mouseStillDownRecipient value: mouseStillDownSelector.
	adder value: mouseUpRecipient value: mouseUpSelector.
	adder value: mouseEnterRecipient value: mouseEnterSelector.
	adder value: mouseLeaveRecipient value: mouseLeaveSelector.
	adder value: mouseEnterDraggingRecipient value: mouseEnterDraggingSelector.
	adder value: mouseLeaveDraggingRecipient value: mouseLeaveDraggingSelector.
	adder value: doubleClickRecipient value: doubleClickSelector.
	adder value: keyStrokeRecipient value: keyStrokeSelector.
	^ list