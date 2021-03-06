removeKey: key ifAbsent: errorBlock 
	"The interpreter might be using this MethodDict while
	this method is running!  Therefore we perform the removal
	in a copy, and then atomically become that copy"
	| copy |
	copy := self copy.
	copy removeDangerouslyKey: key ifAbsent: [^ errorBlock value].
	self become: copy