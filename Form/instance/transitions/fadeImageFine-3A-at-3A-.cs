fadeImageFine: otherImage at: topLeft
	"Display fadeImageFine: (Form fromDisplay: (40@40 extent: 300@300)) reverse at: 40@40"
	| pix j ii d |
	d _ self depth.
	^ self fadeImage: otherImage at: topLeft indexAndMaskDo:
		[:i :mask |
		i=1 ifTrue: [pix _ (1 bitShift: d) - 1.
					1 to: 8//d-1 do:
						[:q | pix _ pix bitOr: (pix bitShift: d*4)]].
		i <= 16 ifTrue:
		[ii _ #(0 10 2 8 7 13 5 15 1 11 3 9 6 12 4 14) at: i.
		j _ ii//4+1.
		(0 to: 28 by: 4) do:
			[:k | mask bits at: j+k put:
				((mask bits at: j+k) bitOr: (pix bitShift: ii\\4*d))].
		true]
		ifFalse: [false]]