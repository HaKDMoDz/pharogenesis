selectMessageCategoryNamed: aSymbol 
	"Given aSymbol, select the category with that name.  Do nothing if 
	aSymbol doesn't exist."
	self messageCategoryListIndex: (self messageCategoryList indexOf: aSymbol ifAbsent: [ 1])