noteOfDuration: duration

	| note |
	note _ self noteInScore.
	^ (owner scorePlayer instrumentForTrack: trackIndex)
			soundForMidiKey: note midiKey
			dur: duration
			loudness: (note velocity asFloat / 127.0)
