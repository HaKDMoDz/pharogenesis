add: wordingString icon: aForm help: helpString subMenu: aMenuMorph 
	"Append the given submenu with the given label."
	| item |
	item := MenuItemMorph new.

	item contents: wordingString.
	item subMenu: aMenuMorph.
	item icon: aForm.
	helpString isNil
		ifFalse: [item setBalloonText: helpString].
	self addMorphBack: item