- aNumber
	"Answer the difference between the receiver and aNumber."
	aNumber isFraction ifTrue:
		[^ self + aNumber negated].
	^ aNumber adaptToFraction: self andSend: #-