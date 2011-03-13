openMIDIFile
	"Open a MIDI score and re-init controls..."
	| score fileName f player |
	fileName := Utilities chooseFileWithSuffixFromList: #('.mid' '.midi')
					withCaption: 'Choose a MIDI file to open' translated.
	(fileName isNil or: [ fileName == #none ])
		ifTrue: [^ self inform: 'No .mid/.midi files found in the Squeak directory' translated].
	f := FileStream readOnlyFileNamed: fileName.
	score := (MIDIFileReader new readMIDIFrom: f binary) asScore.
	f close.
	player := ScorePlayer onScore: score.
	self onScorePlayer: player title: fileName