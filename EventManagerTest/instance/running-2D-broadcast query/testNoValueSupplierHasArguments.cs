testNoValueSupplierHasArguments

	succeeded := eventSource 
		triggerEvent: #needsValue:
		with: 'nelja'
		ifNotHandled: [true].
	self should: [succeeded]