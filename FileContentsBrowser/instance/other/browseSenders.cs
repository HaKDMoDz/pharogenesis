browseSenders
	"Create and schedule a message set browser on all senders of the 
	currently selected message selector. Do nothing if no message is selected."

	messageListIndex ~= 0 
		ifTrue: [Smalltalk browseAllCallsOn: self selectedMessageName]