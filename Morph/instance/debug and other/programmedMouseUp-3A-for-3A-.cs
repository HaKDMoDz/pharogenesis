programmedMouseUp: anEvent for: aMorph 
	| aCodeString |
	self deleteAnyMouseActionIndicators.
	aCodeString := self valueOfProperty: #mouseUpCodeToRun ifAbsent: [^self].
	(self fullBounds containsPoint: anEvent cursorPoint) ifFalse: [^self].
	
	[(aCodeString isMessageSend) 
		ifTrue: [aCodeString value]
		ifFalse: 
			[Compiler 
				evaluate: aCodeString
				for: self
				notifying: nil
				logged: false]] 
			on: ProgressTargetRequestNotification
			do: [:ex | ex resume: self]	"in case a save/load progress display needs a home"