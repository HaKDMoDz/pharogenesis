addCategory
	"Present a choice of categories or prompt for a new category name and add it before the current selection, or at the end if no current selection"
	| labels reject lines cats menuIndex oldIndex newName |
	self okToChange ifFalse: [^ self].
	classListIndex = 0 ifTrue: [^ self].
	labels _ OrderedCollection with: 'new...'.
	reject _ Set new.
	reject
		addAll: self selectedClassOrMetaClass organization categories;
		add: ClassOrganizer nullCategory;
		add: ClassOrganizer default.
	lines _ OrderedCollection new.
	self selectedClassOrMetaClass allSuperclasses do: [:cls |
		cls = Object ifFalse: [
			cats _ cls organization categories reject:
				 [:cat | reject includes: cat].
			cats isEmpty ifFalse: [
				lines add: labels size.
				labels addAll: cats asSortedCollection.
				reject addAll: cats]]].
	newName _ (labels size = 1 or: [
		menuIndex _ (PopUpMenu labelArray: labels lines: lines)
		startUpWithCaption: 'Add Category'.
		menuIndex = 0 ifTrue: [^ self].
		menuIndex = 1])
			ifTrue: [
				self request: 'Please type new category name'
					initialAnswer: 'category name']
			ifFalse: [
				labels at: menuIndex].
	oldIndex _ messageCategoryListIndex.
	newName isEmpty
		ifTrue: [^ self]
		ifFalse: [newName _ newName asSymbol].
	self classOrMetaClassOrganizer
		addCategory: newName
		before: (messageCategoryListIndex = 0
				ifTrue: [nil]
				ifFalse: [self selectedMessageCategoryName]).
	self changed: #messageCategoryList.
	self messageCategoryListIndex:
		(oldIndex = 0
			ifTrue: [self classOrMetaClassOrganizer categories size + 1]
			ifFalse: [oldIndex]).
	self changed: #messageCategoryList.
