addModelYellowButtonMenuItemsTo: aCustomMenu forMorph: aMorph hand: aHandMorph 
	"The receiver serves as the model for aMorph; a menu is being constructed for the morph, and here the receiver is able to add its own items"
	Preferences cmdGesturesEnabled ifTrue: [ "build mode"
		aCustomMenu add: 'inspect model' translated target: self action: #inspect.
	].

	^aCustomMenu
