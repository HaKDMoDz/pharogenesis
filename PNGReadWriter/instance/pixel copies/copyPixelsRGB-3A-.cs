copyPixelsRGB: y 
	"Handle non-interlaced RGB color mode (colorType = 2)"
	| i pixel tempForm tempBits |
	tempForm := Form 
		extent: width @ 1
		depth: 32.
	tempBits := tempForm bits.
	pixel := LargePositiveInteger new: 4.
	pixel 
		at: 4
		put: 255.
	bitsPerChannel = 8 
		ifTrue: 
			[ i := 1.
			1 
				to: width
				do: 
					[ :x | 
					pixel
						at: 3
							put: (thisScanline at: i);
						at: 2
							put: (thisScanline at: i + 1);
						at: 1
							put: (thisScanline at: i + 2).
					tempBits 
						at: x
						put: pixel.
					i := i + 3 ] ]
		ifFalse: 
			[ i := 1.
			1 
				to: width
				do: 
					[ :x | 
					pixel
						at: 3
							put: (thisScanline at: i);
						at: 2
							put: (thisScanline at: i + 2);
						at: 1
							put: (thisScanline at: i + 4).
					tempBits 
						at: x
						put: pixel.
					i := i + 6 ] ].
	transparentPixelValue ifNotNil: 
		[ 1 
			to: width
			do: 
				[ :x | 
				(tempBits at: x) = transparentPixelValue ifTrue: 
					[ tempBits 
						at: x
						put: 0 ] ] ].
	tempForm 
		displayOn: form
		at: 0 @ y
		rule: Form paint