setConstrainedPosition: aPoint hangOut: partiallyOutside
	"Change the position of this morph and and all of its submorphs to aPoint, but don't let me go outside my owner's bounds.  Let me go within two pixels of completely outside if partiallyOutside is true."

	| trialRect delta boundingMorph bRect |
	owner ifNil:[^self].
	trialRect _ aPoint extent: self bounds extent.
	boundingMorph _ self topRendererOrSelf owner.
	delta _ boundingMorph
			ifNil:    [0@0]
			ifNotNil: [
				bRect _ partiallyOutside 
					ifTrue: [boundingMorph bounds insetBy: 
								self extent negated + boundingMorph borderWidth + (2@2)]
					ifFalse: [boundingMorph bounds].
				trialRect amountToTranslateWithin: bRect].
	self position: aPoint + delta.
	self layoutChanged  "So that, eg, surrounding text will readjust"
