undoMove: cmd redo: redo owner: formerOwner bounds: formerBounds predecessor: formerPredecessor 
	"Handle undo and redo of move commands in morphic"

	self owner ifNil: [^Beeper beep].
	redo 
		ifFalse: 
			["undo sets up the redo state first"

			cmd 
				redoTarget: self
				selector: #undoMove:redo:owner:bounds:predecessor:
				arguments: { 
						cmd.
						true.
						owner.
						bounds.
						owner morphPreceding: self}].
	formerOwner ifNotNil: 
			[formerPredecessor ifNil: [formerOwner addMorphFront: self]
				ifNotNil: [formerOwner addMorph: self after: formerPredecessor]].
	self bounds: formerBounds.
	(self isSystemWindow) ifTrue: [self activate]