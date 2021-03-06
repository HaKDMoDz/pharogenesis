removeSystemCategory
	"If a class category is selected, create a Confirmer so the user can 
	verify that the currently selected class category and all of its classes
 	should be removed from the system. If so, remove it."

	systemCategoryListIndex = 0 ifTrue: [^ self].
	self okToChange ifFalse: [^ self].
	(self classList size = 0
		or: [self confirm: 'Are you sure you want to
remove this system category 
and all its classes?'])
		ifTrue: 
		[systemOrganizer removeSystemCategory: self selectedSystemCategoryName.
		self systemCategoryListIndex: 0.
		self changed: #systemCategoryList]