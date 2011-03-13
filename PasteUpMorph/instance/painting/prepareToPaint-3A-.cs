prepareToPaint: stopRunningScripts
	"We're about to start painting. Do a few preparations that make the system more responsive."

	self hideViewerFlaps. "make room"
	stopRunningScripts ifTrue:
		[self setProperty: #scriptsToResume toValue: self presenter allCurrentlyTickingScriptInstantiations.  "We'll restart these when painting is done"
		self stopRunningAll]. "stop scripts"
	self abandonAllHalos. "no more halos"