classList
	"Answer an array of the class names of the selected category. Answer an 
	empty array if no selection exists."

	(systemCategoryListIndex = 0 or:[self selectedPackage isNil])
		ifTrue: [^Array new]
		ifFalse: [^self selectedPackage classes keys asSortedCollection].