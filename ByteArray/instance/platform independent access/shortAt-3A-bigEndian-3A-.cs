shortAt: index bigEndian: aBool
	"Return a 16 bit integer quantity starting from the given byte index"
	| uShort |
	uShort _ self unsignedShortAt: index bigEndian: aBool.
	^(uShort bitAnd: 16r7FFF) - (uShort bitAnd: 16r8000)