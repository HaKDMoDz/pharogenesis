emphasisMenuForFont: font target: target selector: selector highlight: currentEmphasis
	"Offer a font emphasis menu for the given style. If one is selected, pass that font to target with a call to selector. The fonts will be displayed in that font.
	Answer nil if no derivatives exist.
	"

 	| aMenu derivs |
	derivs := font derivativeFonts.
	derivs isEmpty ifTrue: [ ^nil ].
	aMenu _ MenuMorph entitled: 'emphasis' translated.
	derivs := derivs asOrderedCollection.
	derivs addFirst: font.
	derivs do: [ :df | 
			aMenu 
				add: (AbstractFont emphasisStringFor: df emphasis)
				target: target 
				selector: selector
				argument: df.
                aMenu lastItem font: df.
                df emphasis == currentEmphasis ifTrue: [aMenu lastItem color: Color blue darker]].
        ^ aMenu