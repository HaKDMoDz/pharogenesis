showAltSyntax: aBoolean
	"Set the decompile toggle as indicated"

	self contentsSymbol: (aBoolean ifFalse: [#source] ifTrue: [#altSyntax])