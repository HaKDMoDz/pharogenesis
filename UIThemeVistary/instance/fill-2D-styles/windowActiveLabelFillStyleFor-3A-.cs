windowActiveLabelFillStyleFor: aWindow
	"Return the window active label fillStyle for the given window."
	
	^aWindow paneColorToUse alphaMixed: 0.3 with: Color black