addPredecessor: evt
	| newMorph |
	newMorph _ self copy predecessor: predecessor successor: self.
	newMorph extent: self width @ 100.
	predecessor ifNotNil: [predecessor setSuccessor: newMorph].
	self setPredecessor: newMorph.
	predecessor recomposeChain.
	evt hand attachMorph: newMorph