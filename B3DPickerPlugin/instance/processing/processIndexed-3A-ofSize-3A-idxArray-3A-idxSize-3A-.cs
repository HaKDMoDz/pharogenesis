processIndexed: vtxArray ofSize: vtxSize idxArray: idxArray idxSize: idxSize
	| vtxPtr zValue wValue minZ index |
	self returnTypeC:'double'.
	self var: #vtxArray declareC:'float *vtxArray'.
	self var: #vtxPtr declareC:'float *vtxPtr'.
	self var: #idxArray declareC:'int *idxArray'.
	self var: #wValue declareC:'double wValue'.
	self var: #zValue declareC:'double zValue'.
	self var: #minZ declareC:'double minZ'.
	minZ _ 10.0.
	1 to: idxSize do:[:i|
		index _ idxArray at: i.
		index > 0 ifTrue:[
			vtxPtr _ vtxArray + (index-1 * PrimVertexSize).
			zValue _ vtxPtr at: PrimVtxRasterPosZ.
			wValue _ vtxPtr at: PrimVtxRasterPosW.
			wValue = 0.0 ifFalse:[zValue _ zValue / wValue].
			zValue < minZ ifTrue:[minZ _ zValue].
		].
	].
	^minZ