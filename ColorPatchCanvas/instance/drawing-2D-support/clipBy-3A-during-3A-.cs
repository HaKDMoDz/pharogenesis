clipBy: aRectangle during: aBlock
	"Set a clipping rectangle active only during the execution of aBlock.
	Note: In the future we may want to have more general clip shapes - not just rectangles"
	| tempCanvas |
	tempCanvas _ (self copyClipRect: aRectangle).
	aBlock value: tempCanvas.
	foundMorph _ tempCanvas foundMorph.