closeHtmlOn: aStream 
	"put on the given stream the tag to close the html  
	representation of the receiver"
	self htmlSize isZero
		ifFalse: [aStream nextPutAll: '</font>']