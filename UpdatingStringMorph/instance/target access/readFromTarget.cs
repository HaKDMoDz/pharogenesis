readFromTarget
	"Update my readout from my target"

	| v ret |
	(target isNil or: [getSelector isNil]) ifTrue: [^contents].
	ret := self checkTarget.
	ret ifFalse: [^ '0'].
	v := target perform: getSelector.	"scriptPerformer"
	(v isKindOf: Text) ifTrue: [v := v asString].
	^self acceptValueFromTarget: v