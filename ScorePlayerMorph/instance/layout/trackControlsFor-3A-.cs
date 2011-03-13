trackControlsFor: trackIndex

	| r |
	r := self makeRow
		hResizing: #shrinkWrap;
		vResizing: #shrinkWrap.
	r addMorphBack: (self trackNumAndMuteButtonFor: trackIndex).
	r addMorphBack: (Morph new extent: 10@5; color: color).  "spacer"
	r addMorphBack: (self panAndVolControlsFor: trackIndex).
	^ r
