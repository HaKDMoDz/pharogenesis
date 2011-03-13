saveAsSunAudio
	"Create a stereo Sun audio file with the result of performing my score."

	| fileName |
	fileName := FillInTheBlank request: 'New file name?' translated.
	fileName isEmpty ifTrue: [^ self].
	(fileName asLowercase endsWith: '.au') ifFalse: [
		fileName := fileName, '.au'].

	scorePlayer storeSunAudioOnFileNamed: fileName.
