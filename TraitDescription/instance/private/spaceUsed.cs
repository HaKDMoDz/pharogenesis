spaceUsed
	^super spaceUsed + (self hasClassTrait
		ifTrue: [self classTrait spaceUsed] 
		ifFalse: [0])