layoutTopToBottom: aMorph in: newBounds 
	"An optimized top-to-bottom list layout"

	| inset n size extent width height block sum vFill posX posY extra centering extraPerCell last amount minX minY maxX maxY sizeX sizeY first cell props |
	size := properties minCellSize asPoint.
	minX := size x.
	minY := size y.
	size := properties maxCellSize asPoint.
	maxX := size x.
	maxY := size y.
	inset := properties cellInset asPoint y.
	extent := newBounds extent.
	n := 0.
	vFill := false.
	sum := 0.
	width := height := 0.
	first := last := nil.
	block := 
			[:m | 
			props := m layoutProperties ifNil: [m].
			props disableTableLayout 
				ifFalse: 
					[n := n + 1.
					cell := LayoutCell new target: m.
					props vResizing == #spaceFill 
						ifTrue: 
							[cell vSpaceFill: true.
							extra := m spaceFillWeight.
							cell extraSpace: extra.
							sum := sum + extra]
						ifFalse: [cell vSpaceFill: false].
					props hResizing == #spaceFill ifTrue: [vFill := true].
					size := m minExtent.
					sizeX := size x.
					sizeY := size y.
					sizeX < minX ifTrue: [sizeX := minX] ifFalse: [sizeX := sizeX min: maxX].
					sizeY < minY ifTrue: [sizeY := minY] ifFalse: [sizeY := sizeY min: maxY].
					cell cellSize: sizeY.
					first ifNil: [first := cell] ifNotNil: [last nextCell: cell].
					last := cell.
					height := height + sizeY.
					sizeX > width ifTrue: [width := sizeX]]].
	properties reverseTableCells 
		ifTrue: [aMorph submorphsReverseDo: block]
		ifFalse: [aMorph submorphsDo: block].
	n > 1 ifTrue: [height := height + ((n - 1) * inset)].
	(properties vResizing == #shrinkWrap 
		and: [properties rubberBandCells or: [sum isZero]]) 
			ifTrue: [extent := (extent x max: width) @ height].
	(properties hResizing == #shrinkWrap 
		and: [properties rubberBandCells or: [vFill not]]) 
			ifTrue: [extent := width @ (extent y max: height)].
	posX := newBounds left.
	posY := newBounds top.

	"Compute extra horizontal space"
	extra := extent x - width.
	extra := extra max: 0.
	extra > 0 
		ifTrue: 
			[vFill 
				ifTrue: [width := extent x]
				ifFalse: 
					[centering := properties wrapCentering.
					centering == #bottomRight ifTrue: [posX := posX + extra].
					centering == #center ifTrue: [posX := posX + (extra // 2)]]].


	"Compute extra vertical space"
	extra := extent y - height.
	extra := extra max: 0.
	extraPerCell := 0.
	extra > 0 
		ifTrue: 
			[sum isZero 
				ifTrue: 
					["extra space but no #spaceFillers"

					centering := properties listCentering.
					centering == #bottomRight ifTrue: [posY := posY + extra].
					centering == #center ifTrue: [posY := posY + (extra // 2)]]
				ifFalse: [extraPerCell := extra asFloat / sum asFloat]].
	n := 0.
	extra := last := 0.
	cell := first.
	[cell isNil] whileFalse: 
			[n := n + 1.
			height := cell cellSize.
			(extraPerCell > 0 and: [cell vSpaceFill]) 
				ifTrue: 
					[extra := (last := extra) + (extraPerCell * cell extraSpace).
					amount := extra truncated - last truncated.
					height := height + amount].
			cell target layoutInBounds: (posX @ posY extent: width @ height).
			posY := posY + height + inset.
			cell := cell nextCell]