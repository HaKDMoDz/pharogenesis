addDropShadowMenuItems: aMenu hand: aHand
	| menu |
	menu := MenuMorph new defaultTarget: self.
	menu
		addUpdating: #hasDropShadowString
		action: #toggleDropShadow.
	menu addLine.
	menu add: 'shadow color...' translated target: self selector: #changeShadowColor.
	menu add: 'shadow offset...' translated target: self selector: #setShadowOffset:.
	aMenu add: 'drop shadow' translated subMenu: menu.