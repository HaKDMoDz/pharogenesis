toolMenu: evt
	| menu |
	menu _ MenuMorph new.
	menu
		addTitle: 'Tools';
		addStayUpItem.
	{
		{'paint brush'. self toolsForPaintBrush}.
		{'selections'. self toolsForSelection}
	} do: [:each |
		menu add: each first
			target: self
			selector: #setCurrentToolTo:
			argumentList: {each second}].
	menu toggleStayUp: nil.
	menu popUpEvent: evt