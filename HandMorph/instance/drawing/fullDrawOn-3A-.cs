fullDrawOn: aCanvas
	"A HandMorph has unusual drawing requirements:
		1. the hand itself (i.e., the cursor) appears in front of its submorphs
		2. morphs being held by the hand cast a shadow on the world/morphs below
	The illusion is that the hand plucks up morphs and carries them above the world."
	"Note: This version caches an image of the morphs being held by the hand for
	 better performance. This cache is invalidated if one of those morphs changes."

	| disableCaching subBnds |
	self suppressDisplay ifTrue: [^ self].

	disableCaching _ false.
	disableCaching ifTrue: [self nonCachingFullDrawOn: aCanvas. ^ self].

	submorphs isEmpty ifTrue:
		[cacheCanvas _ nil.
		^ self drawOn: aCanvas].  "just draw the hand itself"

	subBnds _ Rectangle merging: (submorphs collect: [:m | m fullBounds]).
	self updateCacheCanvas: aCanvas.
	(cacheCanvas == nil or: [cachedCanvasHasHoles and: [cacheCanvas depth = 1]])
		ifTrue:
		["could not use caching due to translucency; do full draw"
		self nonCachingFullDrawOn: aCanvas. ^ self].

	"draw the shadow"
	aCanvas asShadowDrawingCanvas
		translateBy: self shadowOffset during:[:shadowCanvas|
		cachedCanvasHasHoles
			ifTrue: ["Have to draw the real shadow of the form"
					shadowCanvas paintImage: cacheCanvas form at: subBnds origin]
			ifFalse: ["Much faster if only have to shade the edge of a solid rectangle"
					(subBnds areasOutside: (subBnds translateBy: self shadowOffset negated)) do:
						[:r | shadowCanvas fillRectangle: r color: Color black]]].

	"draw morphs in front of the shadow using the cached Form"
	cachedCanvasHasHoles
		ifTrue: [aCanvas paintImage: cacheCanvas form at: subBnds origin]
		ifFalse: [aCanvas drawImage: cacheCanvas form at: subBnds origin
					sourceRect: cacheCanvas form boundingBox].
	self drawOn: aCanvas.  "draw the hand itself in front of morphs"
