addFillStyleMenuItems: aMenu hand: aHand from: aMorph
	"Add the items for changing the current fill style of the receiver"
	aMenu add: 'choose new graphic' target: self selector: #chooseNewGraphicIn:event: argument: aMorph.
	aMenu add: 'grab new graphic' target: self selector: #grabNewGraphicIn:event: argument: aMorph.
	super addFillStyleMenuItems: aMenu hand: aHand from: aMorph.