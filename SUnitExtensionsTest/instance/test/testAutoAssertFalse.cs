testAutoAssertFalse
	| booleanCondition |
	self assert: self isLogging.
	self should: [ self assert: 1 = 2 description: 'self assert: 1 = 2' ] raise: TestResult failure.
	booleanCondition := (self stream contents subStrings: {Character cr}) last = 'self assert: 1 = 2'.
	self assert: booleanCondition