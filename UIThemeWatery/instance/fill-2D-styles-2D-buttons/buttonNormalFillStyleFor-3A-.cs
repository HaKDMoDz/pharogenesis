buttonNormalFillStyleFor: aButton
	"Return the normal button fillStyle for the given button."
	
	|aColor|
	aColor := aButton colorToUse.
	aColor = aButton paneColor ifTrue: [aColor := self buttonColorFor: aButton].
	^(GradientFillStyle ramp: {
			0.0->Color white.
			0.3->Color white.
			0.4->aColor.
			0.6->Color white.
			1.0->Color white})
		origin: aButton bounds origin;
		direction: 0 @ aButton height;
		radial: false