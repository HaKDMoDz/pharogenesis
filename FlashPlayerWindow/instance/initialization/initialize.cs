initialize
	| aFont |
	super initialize.
	aFont :=  Preferences standardButtonFont.
	self addMorph: (startButton := SimpleButtonMorph new borderWidth: 0;
			label: 'play' translated font: aFont; color: Color transparent;
			actionSelector: #startPlaying; target: self).
	startButton setBalloonText: 'continue playing' translated.

	self addMorph: (stopButton := SimpleButtonMorph new borderWidth: 0;
			label: 'stop' translated font: aFont; color: Color transparent;
			actionSelector: #stopPlaying; target: self).
	stopButton setBalloonText: 'stop playing' translated.

	startButton submorphs first color: Color blue.
	stopButton submorphs first color: Color red.

	self adjustBookControls