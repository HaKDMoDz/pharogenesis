bestAccessToFileName: aFileName andDirectory: aDirectoryOrUrlString
	"Answer an array with a stream and a directory. The directory can be nil."
	
	| dir url |
	dir _ Project squeakletDirectory.
	(dir fileExists: aFileName) ifTrue: [
		^{dir readOnlyFileNamed: aFileName. dir}].

	aDirectoryOrUrlString isString ifFalse: [
		^{aDirectoryOrUrlString readOnlyFileNamed: aFileName. aDirectoryOrUrlString}].

	url _ Url absoluteFromFileNameOrUrlString: aDirectoryOrUrlString.

	(url scheme = 'file') ifTrue: [
		dir := FileDirectory on: url pathForDirectory.
		^{dir readOnlyFileNamed: aFileName. dir}].

	(url path anySatisfy: [:each | each = 'SuperSwikiProj']) ifTrue: [
		dir _ SuperSwikiServer new fullPath: url directoryUrl asString.
		^{dir readOnlyFileNamed: url fileName. nil}].

	^{ServerFile new fullPath: aDirectoryOrUrlString. nil}