newVertex: evt fromHandle: handle afterVert: ix
	"Install a new vertex if there is room."
	(owner insertPointAfter: ix) ifFalse: [^ self "not enough room"].
	super newVertex: evt fromHandle: handle afterVert: ix.
	vertices at: ix+1 put: (owner acceptGraphPoint: evt cursorPoint at: ix+1).
	self computeBounds