testAsTimeStamp
	self assert: (aTime asTimeStamp) = (DateAndTime current midnight + aTime) asTimeStamp
