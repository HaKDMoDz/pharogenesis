install
	"InputEventFetcher install"

	Smalltalk addToStartUpList: self after: Cursor.
	Smalltalk addToShutDownList: self after: Form.

	Default := self new.
	Default startUp
