fileInMethods: aCollection
	"FileIn all methods with selectors taken from aCollection"
	| theClass cat |
	self exists ifFalse:[^self classNotDefined].
	theClass := self realClass.
	aCollection do:[:sel|
		cat := self organization categoryOfElement: sel.
		cat = self removedCategoryName ifFalse:[
			theClass 
				compile: (self sourceCodeAt: sel) 
				classified: cat
				withStamp: (self stampAt: sel)
				notifying: nil.
		].
	].