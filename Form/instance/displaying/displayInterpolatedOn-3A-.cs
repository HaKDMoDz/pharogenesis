displayInterpolatedOn: aForm
	"Display the receiver on aForm, using interpolation if necessary.
		Form fromUser displayInterpolatedOn: Display.
	Note: When scaling we attempt to use bilinear interpolation based
	on the 3D engine. If the engine is not there then we use WarpBlt.
	"
	| engine |
	self extent = aForm extent ifTrue:[^self displayOn: aForm].
	Smalltalk at: #B3DRenderEngine 
		ifPresent:[:engineClass| engine _ (engineClass defaultForPlatformOn: aForm)].
	engine ifNil:[
		"We've got no bilinear interpolation. Use WarpBlt instead"
		(WarpBlt current toForm: aForm)
			sourceForm: self destRect: aForm boundingBox;
			combinationRule: 3;
			cellSize: 2;
			warpBits.
		^self
	].
	"Otherwise use the 3D engine for our purposes"
	engine viewport: aForm boundingBox.
	engine material: (B3DMaterial new emission: Color white).
	engine texture: self.
	engine render: (B3DIndexedQuadMesh new plainTextureRect).
	engine finish.