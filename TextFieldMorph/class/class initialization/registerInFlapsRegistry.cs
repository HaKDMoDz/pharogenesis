registerInFlapsRegistry
	"Register the receiver in the system's flaps registry"
	self environment
		at: #Flaps
		ifPresent: [:cl | cl registerQuad: #(TextFieldMorph  exampleBackgroundField	'Scrolling Field'	'A scrolling data field which will have a different value on every card of the background')
						forFlapNamed: 'Scripting'.]