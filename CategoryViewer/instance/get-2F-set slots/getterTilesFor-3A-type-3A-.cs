getterTilesFor: getterSelector type: aType 
	"Answer classic getter for the given name/type"

	"aPhrase _ nil, assumed"

	| selfTile selector aPhrase |
	(#(#color:sees: #colorSees) includes: getterSelector) 
		ifTrue: [aPhrase := self colorSeesPhrase].
	(#(#getPatchValueIn:) includes: getterSelector)
		ifTrue: [aPhrase _ self patchValuePhrase].
	(#(#getRedComponentIn:) includes: getterSelector)
		ifTrue: [aPhrase _ self colorComponentPhraseFor: #red].
	(#(#getGreenComponentIn:) includes: getterSelector)
		ifTrue: [aPhrase _ self colorComponentPhraseFor: #green].
	(#(#getBlueComponentIn:) includes: getterSelector)
		ifTrue: [aPhrase _ self colorComponentPhraseFor: #blue].
	(#(#getUphillIn:) includes: getterSelector)
		ifTrue: [aPhrase _ self patchUphillPhrase].
	(#(#bounceOn:) includes: getterSelector)
		ifTrue: [aPhrase _ self bounceOnPhrase].
	(#(#bounceOn:color: #bounceOnColor:) includes: getterSelector)
		ifTrue: [aPhrase _ self bounceOnColorPhrase].
	(getterSelector = #getDistanceTo:)
		ifTrue: [aPhrase _ self distanceToPhrase].
	(getterSelector = #getAngleTo:)
		ifTrue: [aPhrase _ self angleToPhrase].
	(getterSelector = #getTurtleOf:)
		ifTrue: [aPhrase _ self turtleOfPhrase].
	(#(#seesColor: #isOverColor) includes: getterSelector) 
		ifTrue: [aPhrase := self seesColorPhrase].
	(#(#overlaps: #overlaps) includes: getterSelector) 
		ifTrue: [aPhrase := self overlapsPhrase].
	(#(#overlapsAny: #overlapsAny) includes: getterSelector) 
		ifTrue: [aPhrase := self overlapsAnyPhrase].
	(#(#touchesA: #touchesA) includes: getterSelector) 
		ifTrue: [aPhrase := self touchesAPhrase].
	aPhrase ifNil: 
			[aPhrase := PhraseTileMorph new setSlotRefOperator: getterSelector asSymbol
						type: aType].
	selfTile := self tileForSelf bePossessive.
	selfTile position: aPhrase firstSubmorph position.
	aPhrase firstSubmorph addMorph: selfTile.
	selector := aPhrase submorphs second.
	(#(#getPatchValueIn: getUphillIn:) includes: getterSelector) ifFalse: [
		(Vocabulary vocabularyNamed: aType capitalized) 
			ifNotNilDo: [:aVocab | aVocab wantsSuffixArrow ifTrue: [selector addSuffixArrow]].
	].
	selector updateLiteralLabel.
	aPhrase enforceTileColorPolicy.
	^aPhrase