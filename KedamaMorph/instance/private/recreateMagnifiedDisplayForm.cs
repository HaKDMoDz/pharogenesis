recreateMagnifiedDisplayForm

	magnifiedDisplayForm _ Form extent: self dimensions * pixelsPerPatch depth: 32.
	self changed.
