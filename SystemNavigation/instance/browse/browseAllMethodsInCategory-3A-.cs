browseAllMethodsInCategory: category 
	^self browseMessageList: (self allMethodsInCategory: category)
		name: category