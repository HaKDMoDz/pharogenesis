translucentImage: aForm at: aPoint sourceRect: sourceRect
	"Draw a translucent image using the best available way of representing translucency.
	Note: This will be fixed in the future."
	self shadowColor ifNotNil:[
		^self stencil: aForm at: aPoint sourceRect: sourceRect color: self shadowColor].
	(self depth < 32 or:[aForm depth < 32]) 
		ifTrue:[^self paintImage: aForm at: aPoint sourceRect: sourceRect].
	self image: aForm
		at: aPoint
		sourceRect: sourceRect
		rule: Form blend