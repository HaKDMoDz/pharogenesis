mouseDown: evt 
	"Handle a mouse down event. The default response is to let my 
	eventHandler, if any, handle it."
	evt yellowButtonPressed
		ifTrue: ["First check for option (menu) click"
			^ self yellowButtonActivity: evt shiftPressed].
	self eventHandler
		ifNotNil: [self eventHandler mouseDown: evt fromMorph: self]
