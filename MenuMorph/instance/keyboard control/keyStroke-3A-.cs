keyStroke: evt 
	| matchString char asc selectable help |
	help _ BalloonMorph string: 'Enter text to\narrow selection down\to matching items ' withCRs for: self corner: #topLeft.
	help popUpForHand: self activeHand.
	(self rootMenu hasProperty: #hasUsedKeyboard) 
		ifFalse: 
			[self rootMenu setProperty: #hasUsedKeyboard toValue: true.
			self changed].
	(evt commandKeyPressed and: [self commandKeyHandler notNil]) 
		ifTrue: 
			[self commandKeyHandler commandKeyTypedIntoMenu: evt.
			^self deleteIfPopUp: evt].
	char := evt keyCharacter.
	asc := char asciiValue.
	char = Character cr 
		ifTrue: 
			[selectedItem ifNotNil: 
					[selectedItem hasSubMenu 
						ifTrue: 
							[evt hand newMouseFocus: selectedItem subMenu.
							^evt hand newKeyboardFocus: selectedItem subMenu]
						ifFalse: 
							["self delete."

							^selectedItem invokeWithEvent: evt]].
			(selectable := self items) size = 1 
				ifTrue: [^selectable first invokeWithEvent: evt].
			^self].
	asc = 27 
		ifTrue: 
			["escape key"

			self valueOfProperty: #matchString
				ifPresentDo: 
					[:str | 
					str isEmpty 
						ifFalse: 
							["If filtered, first ESC removes filter"

							self setProperty: #matchString toValue: String new.
							self selectItem: nil event: evt.
							^self displayFiltered: evt]].
			"If a stand-alone menu, just delete it"
			popUpOwner ifNil: [^self delete].
			"If a sub-menu, then deselect, and return focus to outer menu"
			self selectItem: nil event: evt.
			evt hand newMouseFocus: popUpOwner owner.
			^evt hand newKeyboardFocus: popUpOwner owner].
	(asc = 28 or: [asc = 29]) 
		ifTrue: 
			["left or right arrow key"

			(selectedItem notNil and: [selectedItem hasSubMenu]) 
				ifTrue: 
					[evt hand newMouseFocus: selectedItem subMenu.
					selectedItem subMenu moveSelectionDown: 1 event: evt.
					^evt hand newKeyboardFocus: selectedItem subMenu]].
	asc = 30 ifTrue: [^self moveSelectionDown: -1 event: evt].	"up arrow key"
	asc = 31 ifTrue: [^self moveSelectionDown: 1 event: evt].	"down arrow key"
	asc = 11 ifTrue: [^self moveSelectionDown: -5 event: evt].	"page up key"
	asc = 12 ifTrue: [^self moveSelectionDown: 5 event: evt].	"page down key"
	matchString := self valueOfProperty: #matchString ifAbsentPut: [String new].
	matchString := char = Character backspace 
				ifTrue: 
					[matchString isEmpty ifTrue: [matchString] ifFalse: [matchString allButLast]]
				ifFalse: [matchString copyWith: evt keyCharacter].
	self setProperty: #matchString toValue: matchString.
	self displayFiltered: evt.
	help _ BalloonMorph string: 'Enter text to\narrow selection down\to matching items ' withCRs for: self corner: #topLeft.
	help popUpForHand: self activeHand.
