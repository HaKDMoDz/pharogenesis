decompressSound: aByteArray stereo: stereo samples: numSamples rate: samplingRate into: buffers
	| data nBits signMask indexTable channels valPred index vp idx delta step vpdiff allButSignMask k k0 |
	data _ FlashFileStream on: (ReadStream on: aByteArray).
	data initBits.
	nBits _ (data nextBits: 2) + 2.
	signMask _ 1 bitShift: nBits - 1.
	allButSignMask _ signMask bitInvert32.
	k0 _ 1 bitShift: (nBits - 2).
	indexTable _ IndexTables at: nBits - 1.
	channels _ stereo ifTrue:[2] ifFalse:[1].
	valPred _ IntegerArray new: channels.
	index _ IntegerArray new: channels.
	1 to: numSamples do:[:nOut|
		(nOut bitAnd: 16rFFF) = 1 ifTrue:["New block header starts every 4KB"
			1 to: channels do:[:i|
				vp _ data nextSignedBits: 16.
				valPred at: i put: vp.
				(buffers at: i) nextPut: vp.
				"First sample has no delta"
				index at: i put: (data nextBits: 6).
			].
		] ifFalse:[ "Decode next sample"
			1 to: channels do:[:i|
				vp _ valPred at: i.
				idx _ index at: i.
				"Get next delta value"
				delta _ data nextBits: nBits.
				"Compute difference and new predicted value"
				"Computes 'vpdiff = (delta+0.5)*step/4"
				step _ StepTable at: idx + 1.
				k _ k0.
				vpdiff _ 0.
				[	(delta bitAnd: k) = 0 ifFalse:[vpdiff _ vpdiff + step].
					step _ step bitShift: -1.
					k _ k bitShift: -1.
					k = 0] whileFalse.
				vpdiff _ vpdiff + step.
				(delta anyMask: signMask) 
					ifTrue:[vp _ vp - vpdiff]
					ifFalse:[vp _ vp + vpdiff].
				"Compute new index value"
				idx _ idx + (indexTable at: (delta bitAnd: allButSignMask) + 1).
				"Clamp index"
				idx < 0 ifTrue:[idx _ 0].
				idx > 88 ifTrue:[idx _ 88].
				"Clamp output value"
				vp < -32768 ifTrue:[vp _ -32768].
				vp > 32767 ifTrue:[vp _ 32767].
				"Store values back"
				index at: i put: idx.
				valPred at: i put: vp.
				(buffers at: i) nextPut: vp.
			]
		].
	].