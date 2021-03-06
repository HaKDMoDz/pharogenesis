restoreResumptionTimes
	"Private! Restore the resumption times of all scheduled Delays after a snapshot or clock roll-over. This method should be called only while the AccessProtect semaphore is held."

	| newBaseTime |
	newBaseTime := Time millisecondClockValue.
	SuspendedDelays do: [:d | d adjustResumptionTimeOldBase: 0 newBase: newBaseTime].
	ActiveDelay == nil ifFalse: [
		ActiveDelay adjustResumptionTimeOldBase: 0 newBase: newBaseTime.
	].
