imageForm

	self updateCacheCanvas: Display getCanvas.
	^ cacheCanvas form offset: self fullBounds topLeft
