testErrorWhenDayIsAfterMonthEnd

	self
		should:
			[DateAndTime
				year: 2004
				month: 2
				day: 30]
		raise: Error.

	self
		shouldnt:
			[DateAndTime
				year: 2004
				month: 2
				day: 29]
		raise: Error.
	