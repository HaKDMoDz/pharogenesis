creationTemplateLocation: location user: user password: password
	^
'MCHttpRepository
	location: {1}
	user: {2}
	password: {3}' format: {location printString. user printString. password printString}