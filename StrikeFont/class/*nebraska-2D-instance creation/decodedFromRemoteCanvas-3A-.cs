decodedFromRemoteCanvas: aString

	| array style base |
	array _ aString findTokens: #($ ).
	style _ TextStyle named: (array at: 1) asSymbol.
	style ifNil: [^ TextStyle defaultFont].
	(style fontArray first name = style fontArray first name withoutTrailingDigits) ifTrue: [
			^ self familyName: (array at: 1) size: (array at: 3) asNumber emphasized: (array at: 4) asNumber].
	base _ style fontArray detect: [:f | (array at: 2) beginsWith: f name].
	^ base emphasized: (array at: 4) asNumber.

	"^ self familyName: (array at: 1) size: (array at: 2) asNumber emphasized: (array at: 3) asNumber."
