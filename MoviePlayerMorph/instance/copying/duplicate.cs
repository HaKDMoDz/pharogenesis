duplicate
	| dup |
	playDirection ~= 0 ifTrue: [self stopPlay].
	dup := super duplicate.
	dup scorePlayer: scorePlayer copy.  "Share sound track if any."
	^ dup duplicateMore startStepping