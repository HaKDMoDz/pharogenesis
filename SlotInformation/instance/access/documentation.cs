documentation
	"Answer the receiver's documentation"

	documentation ifNil: [documentation _ 'This is a variable defined by you.  Please edit this into your own meaningful documentation.' translated].
	^ documentation