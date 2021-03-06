testIdentityIndexOfIAbsent
	"self debug: #testIdentityIndexOfIfAbsent"
	| collection element |
	element := self elementInCollectionOfFloat copy.
	self deny: self elementInCollectionOfFloat == element.
	collection := self collectionOfFloat copyWith: element.
	self assert: (collection 
			identityIndexOf: element
			ifAbsent: [ 0 ]) = collection size.
	self assert: (self collectionOfFloat 
			identityIndexOf: element
			ifAbsent: [ 55 ]) = 55