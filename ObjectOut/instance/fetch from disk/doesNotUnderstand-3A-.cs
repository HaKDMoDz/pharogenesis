doesNotUnderstand: aMessage 
	"Bring in the object, install, then resend aMessage"
	| realObject oldFlag response |
	oldFlag _ recursionFlag.
	recursionFlag _ true.
	"fetch the object"
	realObject _ self xxxFetch.		"watch out for the become!"
			"Now we ARE the realObject"
	oldFlag == true ifTrue: [
		response _ (PopUpMenu labels: 'proceed normally\debug' withCRs)
			startUpWithCaption: 'Object being fetched for a second time.
Should not happen, and needs to be fixed later.'.
		response = 2 ifTrue: [self halt]].	"We are already the new object"

	"Can't be a super message, since this is the first message sent to this object"
	^ realObject perform: aMessage selector withArguments: aMessage arguments