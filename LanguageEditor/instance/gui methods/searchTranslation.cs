searchTranslation
	| search |
	search := UIManager default request: 'search for' translated initialAnswer: ''.
	search isEmptyOrNil
		ifTrue: [""
			self beep.
			^ self].
""
self searchTranslation: search