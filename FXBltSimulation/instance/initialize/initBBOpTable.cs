initBBOpTable
	self cCode: 'opTable[0+1] = (int)clearWordwith'.
	self cCode: 'opTable[1+1] = (int)bitAndwith'.
	self cCode: 'opTable[2+1] = (int)bitAndInvertwith'.
	self cCode: 'opTable[3+1] = (int)sourceWordwith'.
	self cCode: 'opTable[4+1] = (int)bitInvertAndwith'.
	self cCode: 'opTable[5+1] = (int)destinationWordwith'.
	self cCode: 'opTable[6+1] = (int)bitXorwith'.
	self cCode: 'opTable[7+1] = (int)bitOrwith'.
	self cCode: 'opTable[8+1] = (int)bitInvertAndInvertwith'.
	self cCode: 'opTable[9+1] = (int)bitInvertXorwith'.
	self cCode: 'opTable[10+1] = (int)bitInvertDestinationwith'.
	self cCode: 'opTable[11+1] = (int)bitOrInvertwith'.
	self cCode: 'opTable[12+1] = (int)bitInvertSourcewith'.
	self cCode: 'opTable[13+1] = (int)bitInvertOrwith'.
	self cCode: 'opTable[14+1] = (int)bitInvertOrInvertwith'.
	self cCode: 'opTable[15+1] = (int)destinationWordwith'.
	self cCode: 'opTable[16+1] = (int)destinationWordwith'.
	self cCode: 'opTable[17+1] = (int)destinationWordwith'.
	self cCode: 'opTable[18+1] = (int)addWordwith'.
	self cCode: 'opTable[19+1] = (int)subWordwith'.
	self cCode: 'opTable[20+1] = (int)rgbAddwith'.
	self cCode: 'opTable[21+1] = (int)rgbSubwith'.
	self cCode: 'opTable[22+1] = (int)OLDrgbDiffwith'.
	self cCode: 'opTable[23+1] = (int)OLDtallyIntoMapwith'.
	self cCode: 'opTable[24+1] = (int)alphaBlendwith'.
	self cCode: 'opTable[25+1] = (int)pixPaintwith'.
	self cCode: 'opTable[26+1] = (int)pixMaskwith'.
	self cCode: 'opTable[27+1] = (int)rgbMaxwith'.
	self cCode: 'opTable[28+1] = (int)rgbMinwith'.
	self cCode: 'opTable[29+1] = (int)rgbMinInvertwith'.
	self cCode: 'opTable[30+1] = (int)alphaBlendConstwith'.
	self cCode: 'opTable[31+1] = (int)alphaPaintConstwith'.
	self cCode: 'opTable[32+1] = (int)rgbDiffwith'.
	self cCode: 'opTable[33+1] = (int)tallyIntoMapwith'.
	self cCode: 'opTable[34+1] = (int)alphaBlendScaledwith'.
	self cCode: 'opTable[35+1] = (int)srcPaintwith'.
	self cCode: 'opTable[36+1] = (int)dstPaintwith'.
