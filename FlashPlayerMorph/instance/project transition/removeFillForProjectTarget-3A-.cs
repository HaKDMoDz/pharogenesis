removeFillForProjectTarget: aFillStyle
	| fillStyles |
	fillStyles _ self valueOfProperty: #projectTargetFills ifAbsent:[^self].
	aFillStyle form: (fillStyles at: aFillStyle ifAbsent:[^self]).
	fillStyles removeKey: aFillStyle.
	self updateProjectFillsFrom: Project current.
	self changed.