interpolateTo: end at: amountDone
	"Interpolate between the instance and end after the specified amount has been done (0 - 1)."

	^ self + ((end - self) * amountDone).