doExpiredHaltCount: aString
	self clearHaltOnce.
	self removeHaltCount.
	self halt: aString