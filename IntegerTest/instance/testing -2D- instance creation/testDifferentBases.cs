testDifferentBases
	"| value |
	2 to: 36 do: [:each|
		value _ 0.
		1 to: each-1 do: [:n| value _ value + (n * (each raisedToInteger: n))].
		value _ value negated.
		Transcript tab; show: 'self assert: (', value printString, ' printStringBase: ', each printString, ') = ''', (value printStringBase: each), '''.'; cr.
		Transcript tab; show: 'self assert: (', value printString, ' radix: ', each printString, ') = ''', (value radix: each), '''.'; cr.
		Transcript tab; show: 'self assert: ', value printString, ' printStringHex = ''', (value printStringBase: 16), '''.'; cr.
		Transcript tab; show: 'self assert: (', value printString, ' storeStringBase: ', each printString, ') = ''', (value storeStringBase: each), '''.'; cr.
		Transcript tab; show: 'self assert: ', value printString, ' storeStringHex = ''', (value storeStringBase: 16), '''.'; cr.


].
	"

	self assert: 2r10 = 2.
	self assert: 3r210 = 21.
	self assert: 4r3210 = 228.
	self assert: 5r43210 = 2930.
	self assert: 6r543210 = 44790.
	self assert: 7r6543210 = 800667.
	self assert: 8r76543210 = 16434824.
	self assert: 9r876543210 = 381367044.
	self assert: 10r9876543210 = 9876543210.
	self assert: 11rA9876543210 = 282458553905.
	self assert: 12rBA9876543210 = 8842413667692.
	self assert: 13rCBA9876543210 = 300771807240918.
	self assert: 14rDCBA9876543210 = 11046255305880158.
	self assert: 15rEDCBA9876543210 = 435659737878916215.
	self assert: 16rFEDCBA9876543210 = 18364758544493064720.
	self assert: 17rGFEDCBA9876543210 = 824008854613343261192.
	self assert: 18rHGFEDCBA9876543210 = 39210261334551566857170.
	self assert: 19rIHGFEDCBA9876543210 = 1972313422155189164466189.
	self assert: 20rJIHGFEDCBA9876543210 = 104567135734072022160664820.
	self assert: 21rKJIHGFEDCBA9876543210 = 5827980550840017565077671610.
	self assert: 22rLKJIHGFEDCBA9876543210 = 340653664490377789692799452102.
	self assert: 23rMLKJIHGFEDCBA9876543210 = 20837326537038308910317109288851.
	self assert: 24rNMLKJIHGFEDCBA9876543210 = 1331214537196502869015340298036888.
	self assert: 25rONMLKJIHGFEDCBA9876543210 = 88663644327703473714387251271141900.
	self assert: 26rPONMLKJIHGFEDCBA9876543210 = 6146269788878825859099399609538763450.
	self assert: 27rQPONMLKJIHGFEDCBA9876543210 = 442770531899482980347734468443677777577.
	self assert: 28rRQPONMLKJIHGFEDCBA9876543210 = 33100056003358651440264672384704297711484.
	self assert: 29rSRQPONMLKJIHGFEDCBA9876543210 = 2564411043271974895869785066497940850811934.
	self assert: 30rTSRQPONMLKJIHGFEDCBA9876543210 = 205646315052919334126040428061831153388822830.
	self assert: 31rUTSRQPONMLKJIHGFEDCBA9876543210 = 17050208381689099029767742314582582184093573615.
	self assert: 32rVUTSRQPONMLKJIHGFEDCBA9876543210 = 1459980823972598128486511383358617792788444579872.
	self assert: 33rWVUTSRQPONMLKJIHGFEDCBA9876543210 = 128983956064237823710866404905431464703849549412368.
	self assert: 34rXWVUTSRQPONMLKJIHGFEDCBA9876543210 = 11745843093701610854378775891116314824081102660800418.
	self assert: 35rYXWVUTSRQPONMLKJIHGFEDCBA9876543210 = 1101553773143634726491620528194292510495517905608180485.
	self assert: 36rZYXWVUTSRQPONMLKJIHGFEDCBA9876543210 = 106300512100105327644605138221229898724869759421181854980.

	self assert: -2r10 = -2.
	self assert: -3r210 = -21.
	self assert: -4r3210 = -228.
	self assert: -5r43210 = -2930.
	self assert: -6r543210 = -44790.
	self assert: -7r6543210 = -800667.
	self assert: -8r76543210 = -16434824.
	self assert: -9r876543210 = -381367044.
	self assert: -10r9876543210 = -9876543210.
	self assert: -11rA9876543210 = -282458553905.
	self assert: -12rBA9876543210 = -8842413667692.
	self assert: -13rCBA9876543210 = -300771807240918.
	self assert: -14rDCBA9876543210 = -11046255305880158.
	self assert: -15rEDCBA9876543210 = -435659737878916215.
	self assert: -16rFEDCBA9876543210 = -18364758544493064720.
	self assert: -17rGFEDCBA9876543210 = -824008854613343261192.
	self assert: -18rHGFEDCBA9876543210 = -39210261334551566857170.
	self assert: -19rIHGFEDCBA9876543210 = -1972313422155189164466189.
	self assert: -20rJIHGFEDCBA9876543210 = -104567135734072022160664820.
	self assert: -21rKJIHGFEDCBA9876543210 = -5827980550840017565077671610.
	self assert: -22rLKJIHGFEDCBA9876543210 = -340653664490377789692799452102.
	self assert: -23rMLKJIHGFEDCBA9876543210 = -20837326537038308910317109288851.
	self assert: -24rNMLKJIHGFEDCBA9876543210 = -1331214537196502869015340298036888.
	self assert: -25rONMLKJIHGFEDCBA9876543210 = -88663644327703473714387251271141900.
	self assert: -26rPONMLKJIHGFEDCBA9876543210 = -6146269788878825859099399609538763450.
	self assert: -27rQPONMLKJIHGFEDCBA9876543210 = -442770531899482980347734468443677777577.
	self assert: -28rRQPONMLKJIHGFEDCBA9876543210 = -33100056003358651440264672384704297711484.
	self assert: -29rSRQPONMLKJIHGFEDCBA9876543210 = -2564411043271974895869785066497940850811934.
	self assert: -30rTSRQPONMLKJIHGFEDCBA9876543210 = -205646315052919334126040428061831153388822830.
	self assert: -31rUTSRQPONMLKJIHGFEDCBA9876543210 = -17050208381689099029767742314582582184093573615.
	self assert: -32rVUTSRQPONMLKJIHGFEDCBA9876543210 = -1459980823972598128486511383358617792788444579872.
	self assert: -33rWVUTSRQPONMLKJIHGFEDCBA9876543210 = -128983956064237823710866404905431464703849549412368.
	self assert: -34rXWVUTSRQPONMLKJIHGFEDCBA9876543210 = -11745843093701610854378775891116314824081102660800418.
	self assert: -35rYXWVUTSRQPONMLKJIHGFEDCBA9876543210 = -1101553773143634726491620528194292510495517905608180485.
	self assert: -36rZYXWVUTSRQPONMLKJIHGFEDCBA9876543210 = -106300512100105327644605138221229898724869759421181854980.