braceWith: a with: b with: c 
	"This method is used in compilation of brace constructs.
	It MUST NOT be deleted or altered."

	| array |
	array _ self new: 3.
	array at: 1 put: a.
	array at: 2 put: b.
	array at: 3 put: c.
	^ array