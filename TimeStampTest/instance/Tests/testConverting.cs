testConverting

	| d t |
	d := '1-10-2000' asDate.
	t := '11:55:00 am' asTime.

	self
		assert: timestamp asSeconds = (d asSeconds + t asSeconds);
		assert: timestamp asDate = d;
		assert: timestamp asTime = t;
		assert: timestamp asTimeStamp == timestamp;
		assert: timestamp dateAndTime = {d. t}.
