listFontNames
	"HostFont listFontNames"
	"List all the OS font names"
	| font fontNames index |
	fontNames := Array new writeStream.
	index := 0.
	
	[ font := self listFontName: index.
	font == nil ] whileFalse: 
		[ fontNames nextPut: font.
		index := index + 1 ].
	^ fontNames contents