registerInFlapsRegistry
	"Register the receiver in the system's flaps registry"
	self environment
		at: #Flaps
		ifPresent: [:cl | cl registerQuad: #(SelectorBrowser			prototypicalToolWindow		'Method Finder'		'A tool for discovering methods by providing sample values for arguments and results')
						forFlapNamed: 'Tools']
