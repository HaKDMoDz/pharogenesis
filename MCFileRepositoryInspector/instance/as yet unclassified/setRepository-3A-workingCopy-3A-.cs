setRepository: aFileBasedRepository workingCopy: aWorkingCopy
	order := self class order.
	repository := aFileBasedRepository.
	self refresh.
	aWorkingCopy
		ifNil: [selectedPackage := self packageList isEmpty ifFalse: [self packageList first]]
		ifNotNil: [ selectedPackage := aWorkingCopy ancestry ancestorString copyUpToLast: $- ].
	MCWorkingCopy addDependent: self.
