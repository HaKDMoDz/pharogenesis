newStandardPartsBinTitled: aTitle includeControls: includeControls
	| aBook aPage aSize |
	aSize _ 360 @ 190.
	aBook _ BookMorph new color: Color blue veryMuchLighter.
	aBook borderWidth: 0.
	aBook removeEverything.
	aBook disableDragNDrop.
	includeControls ifTrue:
		[aBook addMorphBack: (aBook makeMinimalControlsWithColor: Color transparent title: aTitle)].

	self classNamesForStandardPartsBin do:
		[:aList |
			aPage _ self newPageForStandardPartsBin.
			aList do:
				[:sym | aPage addMorphBack: (Smalltalk at: sym) authoringPrototype].
			aPage replaceTallSubmorphsByThumbnails.
			aPage fixLayout.
			aBook insertPage: aPage pageSize: aSize].

	self customPagesForPartsBin do:
		[:pg | aBook insertPage: pg pageSize: aSize].

	self tilesPagesForPartsBin do:
		[:pg | aBook insertPage: pg pageSize: aSize].

	aBook goToPage: 1.
	aBook currentPage addMorphBack: RectangleMorph roundRectPrototype.

	^ aBook