addClassAndSwitchesTo: window at: nominalFractions plus: verticalOffset

	^self
		addAListPane: self buildMorphicClassList 
		to: window 
		at: nominalFractions 
		plus: verticalOffset
