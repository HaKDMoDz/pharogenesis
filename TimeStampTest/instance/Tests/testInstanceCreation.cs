testInstanceCreation

	self 
		should: [ self timestampClass midnight asDuration = (0 hours) ];
		should: [ self timestampClass noon asDuration = (12 hours) ].
