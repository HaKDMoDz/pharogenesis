rotateZ: angle
	| camera matrix |
	camera := scene defaultCamera.
	matrix := B3DMatrix4x4
		rotatedBy: angle
		around: (camera position - camera target)
		centeredAt: camera target.
	camera up: (matrix localPointToGlobal: camera up).
	self updateHeadlight.
	self changed.