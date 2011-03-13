initialize
	"ColorPickerMorph initialize"

	ColorChart _ ColorPickerMorph colorPaletteForDepth: 16 extent: 190@60.
	DragBox _  (11@0) extent: 9@8.
	RevertBox _ (ColorChart width - 20)@1 extent: 9@8.
	FeedbackBox _ (ColorChart width - 10)@1 extent: 9@8.
	TransparentBox _ DragBox topRight corner: RevertBox bottomLeft.

		ColorChart fillBlack: ((DragBox left - 1)@0 extent: 1@9).
		ColorChart fillBlack: ((TransparentBox left)@0 extent: 1@9).
		ColorChart fillBlack: ((FeedbackBox left - 1)@0 extent: 1@9).
		ColorChart fillBlack: ((RevertBox left - 1)@0 extent: 1@9).
		(Form dotOfSize: 5) displayOn: ColorChart at: DragBox center + (0@1).

	self localeChanged.