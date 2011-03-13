chooseRecentClipping  "Clipboard chooseRecentClipping"
	"Choose by menu from among the recent clippings"

	recent ifNil: [^ nil].
	^ (SelectionMenu
		labelList: (recent collect: [:txt | ((txt asString contractTo: 50)
									copyReplaceAll: Character cr asString with: '\')
									copyReplaceAll: Character tab asString with: '|'])
		selections: recent) startUp.

