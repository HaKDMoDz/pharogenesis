qDecompress: byteArray
	"Decompress strings compressed by qCompress:.
	Most common 12 chars get values 0-11 packed in one 4-bit nibble;
	others get values 12-15 (2 bits) * 16 plus next nibble"
	|  charTable extended ext |
	charTable _  "Character encoding table must match qCompress:"
	' eatrnoislcm bdfghjkpquvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789'.
	^ String streamContents:
		[:strm | extended _ false.  "Flag for 2-nibble characters"
		byteArray do:
			[:byte | 
			(Array with: byte//16 with: byte\\16)
				do:
				[:nibble | extended
					ifTrue: [strm nextPut: (charTable at: ext*16+nibble + 1). extended _ false]
					ifFalse: [nibble < 12 ifTrue: [strm nextPut: (charTable at: nibble + 1)]
									ifFalse: [ext _ nibble-12.  extended _ true]]]]]