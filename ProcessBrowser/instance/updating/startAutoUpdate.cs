startAutoUpdate
	self isAutoUpdatingPaused ifTrue: [ ^autoUpdateProcess resume ].
	self isAutoUpdating
		ifFalse: [| delay | 
			delay := Delay forSeconds: 2.
			autoUpdateProcess := [[self hasView]
						whileTrue: [delay wait.
							deferredMessageRecipient ifNotNil: [
								deferredMessageRecipient addDeferredUIMessage: [self updateProcessList]]
							ifNil: [ self updateProcessList ]].
					autoUpdateProcess := nil] fork].
	self updateProcessList