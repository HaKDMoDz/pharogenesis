glyphInfoOf: aCharacter into: glyphInfoArray
	"Answer the width of the argument as a character in the receiver."

	| form |
	(self hasGlyphOf: aCharacter) ifFalse: [
		^ self fallbackFont glyphInfoOf: aCharacter into: glyphInfoArray.
	].
	form _ self formOf: aCharacter.
	glyphInfoArray at: 1 put: form;
		at: 2 put: 0;
		at: 3 put: form width;
		at: 4 put: (self ascentOf: aCharacter);
		at: 5 put: self.
	^ glyphInfoArray.
