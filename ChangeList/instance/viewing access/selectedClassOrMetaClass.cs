selectedClassOrMetaClass
	| c |
	^ (c := self currentChange) ifNotNil: [c methodClass]