icon
	"Answer a form with an icon to represent the receiver"
	^ self isWorldMorph
		ifTrue: [MenuIcons homeIcon]
		ifFalse: [MenuIcons projectIcon]