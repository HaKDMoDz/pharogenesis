checkCoherence
	"If I recreate the class then don't remove it"

	(changeTypes includes: #remove) ifTrue:
		[changeTypes remove: #remove.
		changeTypes add: #change].
	(changeTypes includes: #addedThenRemoved) ifTrue:
		[changeTypes remove: #addedThenRemoved.
		changeTypes add: #add].
