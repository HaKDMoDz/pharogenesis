colormapIfNeededFor: destForm
	| newMap color pv |
	colors == nil ifTrue: [
		"use the standard colormap"
		^ super colormapIfNeededFor: destForm].

	(destForm depth = cachedDepth and:[cachedColormap isColormap]) 
		ifTrue: [^ cachedColormap].
	newMap _ WordArray new: (1 bitShift: depth).
	1 to: colors size do: [:i |
		color _ colors at: i.
		pv _ destForm pixelValueFor: color.
		(pv = 0 and:[color isTransparent not]) ifTrue:[pv _ 1].
		newMap at: i put: pv].

	cachedDepth _ destForm depth.
	^cachedColormap _ ColorMap shifts: nil masks: nil colors: newMap.