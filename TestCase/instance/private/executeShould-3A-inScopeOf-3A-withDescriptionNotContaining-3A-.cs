executeShould: aBlock inScopeOf: anExceptionalEvent withDescriptionNotContaining: aString
	^[aBlock value.
 	false] on: anExceptionalEvent
		do: [:ex | ex return: (ex description includesSubString: aString) not ]
			