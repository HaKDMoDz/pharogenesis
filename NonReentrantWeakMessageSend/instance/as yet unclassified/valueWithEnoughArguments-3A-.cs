valueWithEnoughArguments: anArray
	"Answer the superclass value or nil if already executing."

	^self execute: [super valueWithEnoughArguments: anArray]