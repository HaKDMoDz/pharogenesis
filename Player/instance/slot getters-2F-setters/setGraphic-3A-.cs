setGraphic: aForm
	"Set the receiver's graphic as indicated"

	| aMorph |
	^ ((aMorph _ costume renderedMorph) isSketchMorph)
		ifTrue:
			[aMorph form: aForm]
		ifFalse:
			[aMorph isPlayfieldLike
				ifTrue: 
					[aMorph backgroundForm: aForm]
				ifFalse:
					["what to do?"]]