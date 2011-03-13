editComment
	"Retrieve the description of the class comment."

	classListIndex = 0 ifTrue: [^ self].
	self okToChange ifFalse: [^ self].
	self messageCategoryListIndex: 0.
	metaClassIndicated := false.
	self editSelection: #editComment.
	self changed: #classSelectionChanged.
	self changed: #messageCategoryList.
	self changed: #messageList.
	self decorateButtons.
	self contentsChanged
