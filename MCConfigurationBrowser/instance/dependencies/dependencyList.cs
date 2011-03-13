dependencyList
	^self dependencies collect: [:dep | 
		Text string: dep versionInfo name
			attributes: (Array streamContents: [:attr |
				dep isFulfilledByAncestors
					ifFalse: [attr nextPut: TextEmphasis bold]
					ifTrue: [dep isCurrent ifFalse: [attr nextPut: TextEmphasis italic]].
			])]
