on: eventName send: selector to: recipient
	self eventHandler ifNil: [self eventHandler: EventHandler new].
	self eventHandler on: eventName send: selector to: recipient