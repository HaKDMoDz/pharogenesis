newTreeIn: aThemedMorph for: aModel list: listSelector selected: getSelector changeSelected: setSelector
	"Answer a new tree morph."
	
	^TreeListMorph new
		selectionColor: self selectionColor;
		font: self listFont;
		on: aModel
		list: listSelector
		selected: getSelector
		changeSelected: setSelector
		menu: nil
		keystroke: nil;
		cornerStyle: #square;
		color: Color white;
		borderStyle: (BorderStyle inset width: 1; baseColor: Color black);
		hResizing: #spaceFill;
		vResizing: #spaceFill;
		autoDeselect: false;
		yourself