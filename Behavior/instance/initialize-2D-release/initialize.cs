initialize
	"moved here from the class side's #new"
	self methodDictionary: self emptyMethodDictionary.
	self superclass: Object.
	self setFormat: Object format