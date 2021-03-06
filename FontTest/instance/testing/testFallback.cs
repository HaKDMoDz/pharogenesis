testFallback
	"self debug: #testFallback"
	| text font bb destPoint |
	text := (Character value: 257) asString asText.
	font := TextStyle default fontOfSize: 21.
	text addAttribute: (TextFontReference toFont: font).
	bb := (Form extent: 100 @ 30) getCanvas privatePort.
	bb combinationRule: Form paint.

	font installOn: bb foregroundColor: Color black backgroundColor: Color white.
	destPoint := font displayString: text on: bb from: 1 to: 1 at: 0@0 kern: 1.

	"bb destForm asMorph openInHand."
	self assert: destPoint x = ((font widthOf: $?) + 1).
