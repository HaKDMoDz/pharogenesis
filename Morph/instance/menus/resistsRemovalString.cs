resistsRemovalString
	"Answer the string to be shown in a menu to represent the 
	'resistsRemoval' status"
	^ (self resistsRemoval
		ifTrue: ['<on>']
		ifFalse: ['<off>']), 'resist being deleted' translated