setFillColor: aColor
	"Install a new color used for filling."
	| screen patternWord fillColor |
	fillColor _ self shadowColor ifNil:[aColor].
	fillColor ifNil:[fillColor _ Color transparent].
	fillColor isColor ifFalse:[
		(fillColor isKindOf: InfiniteForm) ifFalse:[^self error:'Cannot install color'].
		^port fillPattern: fillColor; combinationRule: Form over].
	"Okay, so fillColor really *is* a color"
	port sourceForm: nil.
	fillColor isTranslucent ifFalse:[
		port combinationRule: Form over.
		port fillPattern: fillColor.
		self depth = 8 ifTrue:[
			"In 8 bit depth it's usually a good idea to use a stipple pattern"
			port fillColor: (form balancedPatternFor: fillColor)].
		^self].
	"fillColor is some translucent color"

	self depth > 8 ifTrue:[
		"BitBlt setup for alpha masked transfer"
		port fillPattern: fillColor.
		self depth = 16
			ifTrue:[port alphaBits: fillColor privateAlpha; combinationRule: 30]
			ifFalse:[port combinationRule: Form blend].
		^self].
	"Can't represent actual transparency -- use stipple pattern"
	screen _ Color translucentMaskFor: fillColor alpha depth: self depth.
	patternWord _ form pixelWordFor: fillColor.
	port fillPattern: (screen collect: [:maskWord | maskWord bitAnd: patternWord]).
	port combinationRule: Form paint.
