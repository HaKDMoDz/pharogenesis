removeMessage
	| messageName |
	messageListIndex = 0
		ifTrue: [^ self].
	self okToChange
		ifFalse: [^ self].
	messageName _ self selectedMessageName.
	(self selectedClass confirmRemovalOf: messageName)
		ifFalse: [^ false].
	self selectedClassOrMetaClass removeMethod: self selectedMessageName.
	self messageListIndex: 0.
	self setClassOrganizer.
	"In case organization not cached"
	self changed: #messageList