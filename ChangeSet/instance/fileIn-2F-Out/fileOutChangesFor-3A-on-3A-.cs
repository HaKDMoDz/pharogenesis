fileOutChangesFor: class on: stream 
	"Write out all the method changes for this class."

	| changes |
	changes _ Set new.
	(self methodChangesAtClass: class name) associationsDo: 
		[:mAssoc | (mAssoc value = #remove or: [mAssoc value = #addedThenRemoved])
			ifFalse: [changes add: mAssoc key]].
	changes isEmpty ifFalse: 
		[class fileOutChangedMessages: changes on: stream.
		stream cr]