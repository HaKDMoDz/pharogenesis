findAMessageNamesWindow: evt
	"Locate a MessageNames tool, open it, and bring it to the front.  Create one if necessary"

	self findAWindowSatisfying:
		[:aWindow | aWindow model isKindOf: MessageNames] orMakeOneUsing: [MessageNames new inMorphicWindowLabeled: 'Message Names']