testAddAllLast
	"Allows one to add each element of an orderedCollection at the beginning of another
	orderedCollection "
	"self run:#testAddAllLast" 
	
	| c1 c2 |
	c1 := #(1 2 3 4 ) asOrderedCollection.
	c2 := #(5 6 7 8 9 ) asOrderedCollection.
	c1 addAllLast: c2.
	self assert: c1 = #(1 2 3 4 5 6 7 8 9) asOrderedCollection