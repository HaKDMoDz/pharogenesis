drawOn: aCanvas
	"Draw the menu.  Add keyboard-focus feedback if appropriate"

	super drawOn: aCanvas.
	(ActiveHand notNil and: [ActiveHand keyboardFocus == self] and: [self rootMenu hasProperty: #hasUsedKeyboard])
		ifTrue:
		[aCanvas frameAndFillRectangle: self innerBounds fillColor: Color transparent
				borderWidth: 1 borderColor: Preferences keyboardFocusColor]