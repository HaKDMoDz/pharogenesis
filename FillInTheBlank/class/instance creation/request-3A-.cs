request: queryString 
	"Create an instance of me whose question is queryString. Invoke it 
	centered at the cursor, and answer the string the user accepts. Answer 
	the empty string if the user cancels."

	"FillInTheBlank request: 'Your name?'"

	^ self
		request: queryString
		initialAnswer: ''
		centerAt: (ActiveHand ifNil:[Sensor]) cursorPoint