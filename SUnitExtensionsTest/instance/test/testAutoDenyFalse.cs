testAutoDenyFalse
	| booleanCondition |
	self assert: self isLogging.
	self should: [ self deny: 1 = 1 description: 'self deny: 1 = 1'.] raise: TestResult failure.
	booleanCondition := (self stream contents subStrings:  {Character cr}) last = 'self deny: 1 = 1'.
	self assert: booleanCondition