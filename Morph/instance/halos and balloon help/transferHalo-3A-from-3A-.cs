transferHalo: event from: formerHaloOwner
	"Progressively transfer the halo to the next likely recipient"
	| localEvt w target |

	self flag: #workAround. "For halo's distinction between 'target' and 'innerTarget' we need to bypass any renderers."
	(formerHaloOwner == self and:[self isRenderer and:[self wantsHaloFromClick not]]) ifTrue:[
		event shiftPressed ifTrue:[
			target _ owner.
			localEvt _ event transformedBy: (self transformedFrom: owner).
		] ifFalse:[
			target _ self renderedMorph.
			localEvt _ event transformedBy: (target transformedFrom: self).
		].
		^target transferHalo: localEvt from: target].

	"Never transfer halo to top-most world"
	(self isWorldMorph and:[owner isNil]) ifFalse:[
		(self wantsHaloFromClick and:[formerHaloOwner ~~ self]) 
			ifTrue:[^self addHalo: event from: formerHaloOwner]].

	event shiftPressed ifTrue:[
		"Pass it outwards"
		owner ifNotNil:[^owner transferHalo: event from: formerHaloOwner].
		"We're at the top level; throw the event back in to find recipient"
		formerHaloOwner removeHalo.
		^self processEvent: event copy resetHandlerFields.
	].
	self submorphsDo:[:m|
		localEvt _ event transformedBy: (m transformedFrom: self).
		(m fullContainsPoint: localEvt position) 
			ifTrue:[^m transferHalo: event from: formerHaloOwner].
	].
	"We're at the bottom most level; throw the event back up to the root to find recipient"
	formerHaloOwner removeHalo.
	(w _ self world) ifNil: [ ^self ].
	localEvt _ event transformedBy: (self transformedFrom: w) inverseTransformation.
	^w processEvent: localEvt resetHandlerFields.
