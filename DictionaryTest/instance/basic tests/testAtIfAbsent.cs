testAtIfAbsent
	"self run: #testAtIfAbsent"
	
	| dict |
	dict := Dictionary new.
	dict at: #a put: 666.
	
	self assert: (dict at: #a ifAbsent: [nil]) = 666.
	
	self assert: (dict at: #b ifAbsent: [nil]) isNil.

	