copyLoopPixels
	"This version of the inner loop maps source pixels and dest pixels one at a time. This is the most general (and slowest) version which must also keep track of source and dest paint mode by itself."
	| nPix srcShift dstShift destWord srcIndex dstIndex nLines sourceWord lastSrcPix sourcePix srcMask dstMask srcMapped lastDstPix destPix dstMapped resultMapped resultPix srcPaint dstPaint paintMode mergeFn |
	self inline: false.
	mergeFn _ opTable at: combinationRule+1.
	srcPaint _ srcKeyMode.
	dstPaint _ dstKeyMode.
	paintMode _ srcPaint | dstPaint.

	"Additional inits"
	srcMask _ maskTable at: sourceDepth.
	dstMask _ maskTable at: destDepth.
	sourceIndex _ srcIndex _ sourceBits + (sy * sourcePitch) + ((sx // sourcePPW) *4).
	dstIndex _ destIndex.

	"Precomputed shifts for pickSourcePixels"
	srcShift _ ((sx bitAnd: sourcePPW - 1) * sourceDepth).
	dstShift _ ((dx bitAnd: destPPW - 1) * destDepth).
	sourceMSB ifTrue:[srcShift _ 32 - sourceDepth - srcShift].
	destMSB ifTrue:[dstShift _ 32 - destDepth - dstShift].

	srcBitShift _ srcShift.
	dstBitShift _ dstShift.

	noSourceMap 
		ifTrue:[pixelDepth _ sourceDepth]
		ifFalse:[pixelDepth _ 32].
	destMask _ -1.
	nLines _ bbH.
	["this is the vertical loop"
		sourceWord _ self srcLongAt: srcIndex.
		destWord _ self dstLongAt: dstIndex.
		"Prefetch first source pixel"
		lastSrcPix _ sourcePix _ sourceWord >> srcShift bitAnd: srcMask.
		srcMapped _ self mapSourcePixel: sourcePix.
		"Prefetch first dest pixel"
		lastDstPix _ destPix _ destWord >> dstShift bitAnd: dstMask.
		dstMapped _ self mapDestPixel: destPix.
		nPix _ bbW.
		["this is the horizontal loop"
			(paintMode) ifTrue:[
				((srcPaint and:[sourcePix = sourceAlphaKey])
					or:[dstPaint and:[destPix ~= destAlphaKey]])
						ifTrue:[resultMapped _ dstMapped]
						ifFalse:[	resultMapped _ self merge: srcMapped with: dstMapped function: mergeFn].
			] ifFalse:[
				resultMapped _ self merge: srcMapped with: dstMapped function: mergeFn.
			].
			(noColorMap and:[resultMapped = dstMapped]) ifFalse:[
				resultPix _ self mapPixel: resultMapped.
				destWord _ destWord bitAnd: (dstMask << dstShift) bitInvert32.
				destWord _ destWord bitOr: (resultPix bitAnd: dstMask) << dstShift.
			].
			sourceMSB ifTrue:[
				"Adjust source if at pixel boundary"
				(srcShift _ srcShift - sourceDepth) < 0 ifTrue:
					[srcShift _ srcShift + 32.
					sourceWord _ self srcLongAt: (srcIndex _ srcIndex + 4)].
			] ifFalse:[
				"Adjust source if at pixel boundary"
				(srcShift _ srcShift + sourceDepth) > 31 ifTrue:
					[srcShift _ srcShift - 32.
					sourceWord _ self srcLongAt: (srcIndex _ srcIndex + 4)].
			].
			destMSB ifTrue:[
				"Adjust dest if at pixel boundary"
				(dstShift _ dstShift - destDepth) < 0 ifTrue:
					[dstShift _ dstShift + 32.
					self dstLongAt: dstIndex put: destWord.
					destWord _ self dstLongAt: (dstIndex _ dstIndex + 4)].
			] ifFalse:[
				"Adjust dest if at pixel boundary"
				(dstShift _ dstShift + destDepth) > 31 ifTrue:
					[dstShift _ dstShift - 32.
					self dstLongAt: dstIndex put: destWord.
					destWord _ self dstLongAt: (dstIndex _ dstIndex + 4)].
			].
		(nPix _ nPix - 1) = 0] whileFalse:[
			"Fetch next source/dest pixel"
			sourcePix _ sourceWord >> srcShift bitAnd: srcMask.
			lastSrcPix = sourcePix ifFalse:[
				srcMapped _ self mapSourcePixel: sourcePix.
				lastSrcPix _ sourcePix].
			destPix _ destWord >> dstShift bitAnd: dstMask.
			lastDstPix = destPix ifFalse:[
				dstMapped _ self mapDestPixel: destPix.
				lastDstPix _ destPix]
		].
	(nLines _ nLines - 1) = 0] whileFalse:[
		"Store last destWord"
		self dstLongAt: dstIndex put: destWord.
		"Advance sourceIndex, destIndex"
		srcIndex _ sourceIndex _ sourceIndex + sourcePitch.
		dstIndex _ destIndex _ destIndex + destPitch.
		srcShift _ srcBitShift.
		dstShift _ dstBitShift.
	].
	"Store final destWord"
	self dstLongAt: dstIndex put: destWord.