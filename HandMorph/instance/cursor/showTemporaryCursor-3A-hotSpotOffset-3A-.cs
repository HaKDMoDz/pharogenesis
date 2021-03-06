showTemporaryCursor: cursorOrNil hotSpotOffset: hotSpotOffset 
	"Set the temporary cursor to the given Form.
	If the argument is nil, revert to the normal hardware cursor."

	self changed.
	temporaryCursorOffset 
		ifNotNil: [bounds := bounds translateBy: temporaryCursorOffset negated].
	cursorOrNil isNil 
		ifTrue: [temporaryCursor := temporaryCursorOffset := hardwareCursor := nil]
		ifFalse: 
			[temporaryCursor := cursorOrNil asCursorForm.
			temporaryCursorOffset := temporaryCursor offset - hotSpotOffset.
			(cursorOrNil isKindOf: Cursor) ifTrue: [hardwareCursor := cursorOrNil]].
	bounds := self cursorBounds.
	self
		userInitials: userInitials andPicture: self userPicture;
		layoutChanged;
		changed