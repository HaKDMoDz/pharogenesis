toggleShowDocumentation
	"Toggle the setting of the showingDocumentation flag, unless there are unsubmitted edits that the user declines to discard"

	| wasShowing |
	self okToChange ifTrue:
		[wasShowing := self showingDocumentation.
		self showDocumentation: wasShowing not.
		self setContentsToForceRefetch.
		self contentsChanged]

