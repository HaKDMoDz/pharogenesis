decodeValueFrom: table
	"Decode the next value in the receiver using the given huffman table."
	| bits bitsNeeded tableIndex value |
	bitsNeeded := (table at: 1) bitShift: -24.	"Initial bits needed"
	tableIndex := 2.							"First real table"
	[bits := self nextSingleBits: bitsNeeded.	"Get bits"
	value := table at: (tableIndex + bits).		"Lookup entry in table"
	(value bitAnd: 16r3F000000) = 0] 			"Check if it is a non-leaf node"
		whileFalse:["Fetch sub table"
			tableIndex := value bitAnd: 16rFFFF.	"Table offset in low 16 bit"
			bitsNeeded := (value bitShift: -24) bitAnd: 255. "Additional bits in high 8 bit"
			bitsNeeded > MaxBits ifTrue:[^self error:'Invalid huffman table entry']].
	^value