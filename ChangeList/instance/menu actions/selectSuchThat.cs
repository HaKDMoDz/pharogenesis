selectSuchThat
	"query the user for a selection criterio.  By Lex Spoon.  NB: the UI for invoking this from a changelist browser is currently commented out; to reenfranchise it, you'll need to mild editing to ChangeList method #changeListMenu:"
	| code block |
	code := UIManager default request: 'selection criteria for a change named aChangeRecord?\For instance, ''aChangeRecord category = ''System-Network''''' withCRs.

	code isEmpty ifTrue: [^ self ].

	block := Compiler evaluate: '[:aChangeRecord | ', code, ']'.

	self selectSuchThat: block