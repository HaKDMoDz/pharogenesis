displayStringR2L: aString on: aBitBlt from: startIndex to: stopIndex at: aPoint kern: kernDelta 

	| destPoint font form encoding char charCode glyphInfo |
	destPoint := aPoint.
	glyphInfo := Array new: 5.
	startIndex to: stopIndex do: [:charIndex |
		char := aString at: charIndex.
		encoding := char leadingChar + 1.
		charCode := char charCode.
		font := fontArray at: encoding.
		((charCode between: font minAscii and: font maxAscii) not) ifTrue: [
			charCode := font maxAscii].
		self glyphInfoOf: char into: glyphInfo.
		form := glyphInfo at: 1.
			(glyphInfo size > 4 and: [(glyphInfo at: 5) notNil and: [(glyphInfo at: 5) ~= aBitBlt lastFont]]) ifTrue: [
				(glyphInfo at: 5) installOn: aBitBlt.
			].
		aBitBlt sourceForm: form.
		aBitBlt destX: destPoint x - form width.
		aBitBlt destY: destPoint y.
		aBitBlt sourceOrigin: 0 @ 0.
		aBitBlt width: form width.
		aBitBlt height: form height.
		aBitBlt copyBits.
		destPoint := destPoint - (form width + kernDelta @ 0).
	].
