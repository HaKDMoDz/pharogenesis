selectedMessageName
	| aList |
	"Answer the message selector of the currently selected message, if any. 
	Answer nil otherwise."

	messageListIndex = 0 ifTrue: [^ nil].
	^ (aList _ self messageList) size >= messageListIndex
		ifTrue:
			[aList at: messageListIndex]
		ifFalse:
			[nil]