startRunningScripts

	self stopButtonState: false.
	self stepButtonState: false.
	self goButtonState: true.
	associatedMorph startRunningAll.
	associatedMorph borderColor: Preferences borderColorWhenRunning.

	ThumbnailMorph recursionReset.  "needs to be done once in a while"