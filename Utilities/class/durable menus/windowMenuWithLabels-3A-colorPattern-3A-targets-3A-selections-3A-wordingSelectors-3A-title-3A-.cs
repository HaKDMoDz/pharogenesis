windowMenuWithLabels: labelList colorPattern: colorPattern  targets: targetList selections: selectionList wordingSelectors: wordingList title: aTitle
	| aWorld colorList  pos delta aButton  rightmost widthToUse wordingSelector |
	Smalltalk verifyMorphicAvailability ifFalse: [^ self].
	aWorld _ MVCWiWPasteUpMorph newWorldForProject: nil.
	colorList _  (1 to: labelList size) collect:
		[:ind | Color colorFrom: (colorPattern at: (ind \\ colorPattern size + 1))].
			
	pos _ 4 @ 6.
	delta _ 0 @ 30.
	rightmost _ 0.

	1 to: labelList size do:
		[:index |
			wordingSelector _ wordingList at: index.
			aButton _ wordingSelector
				ifNil:
					[SimpleButtonMorph new]
				ifNotNil:
					[UpdatingSimpleButtonMorph new].
			aButton label: (labelList at: index); 
				color: (colorList at: index); 
				target: (targetList at: index);
				actionSelector: (selectionList at: index);
				position: pos.
			wordingSelector ifNotNil: [aButton wordingSelector: wordingSelector].
			rightmost _ rightmost max: aButton right.
			pos _ pos + delta.
			aWorld addMorphBack: aButton].
	widthToUse _ rightmost + 10.
	aWorld startSteppingSubmorphsOf: aWorld.
	aWorld submorphs do:
		[:m | m position: (((widthToUse - m width) // 2) @ m position y)].
	aWorld setProperty: #initialExtent toValue: (widthToUse @ (aButton bottom + 10)).
	aWorld openWithTitle: aTitle cautionOnClose: false