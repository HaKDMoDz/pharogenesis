scanCharactersFrom: startIndex to: stopIndex in: sourceString rightX: rightX stopConditions: stops kern: kernDelta

	| startEncoding selector |
	(sourceString isByteString) ifTrue: [^ self basicScanCharactersFrom: startIndex to: stopIndex in: sourceString rightX: rightX stopConditions: stops kern: kernDelta.].

	(sourceString isWideString) ifTrue: [
		startIndex > stopIndex ifTrue: [lastIndex := stopIndex. ^ stops at: EndOfRun].
		startEncoding :=  (sourceString at: startIndex) leadingChar.
		selector := (EncodedCharSet charsetAt: startEncoding) scanSelector.
		^ self perform: selector withArguments: (Array with: startIndex with: stopIndex with: sourceString with: rightX with: stopConditions with: kernDelta).
	].
	
	^ stops at: EndOfRun
