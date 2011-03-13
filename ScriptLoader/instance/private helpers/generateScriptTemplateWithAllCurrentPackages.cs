generateScriptTemplateWithAllCurrentPackages
	"ScriptLoader new generateScriptTemplateWithAllCurrentPackages"
	
	| str |
	str := ReadWriteStream on: (String new: 1000).
	str nextPutAll: 'scriptXXX' ; cr ; cr ; tab.
	str nextPutAll: '| names|'; cr.
	str nextPutAll: 'names := '.
	str nextPut: $'.
	self currentVersions do: 
		[:each |
			str nextPutAll: each ; nextPutAll: '.mcz']
		separatedBy: [str nextPut: Character cr].
	str nextPut: $'; nextPut: Character cr.
	str nextPutAll: 'findTokens: '' '', String cr.

	self loadTogether: names merge: false.'.
	^ str contents