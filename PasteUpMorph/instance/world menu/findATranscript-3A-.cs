findATranscript: evt
	"Locate a transcript, open it, and bring it to the front.  Create one if necessary"

	self findAWindowSatisfying:
		[:aWindow | aWindow model == Transcript] orMakeOneUsing: [Transcript openAsMorphLabel: 'Transcript']