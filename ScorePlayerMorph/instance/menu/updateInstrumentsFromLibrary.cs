updateInstrumentsFromLibrary
	"The instrument library has been modified. Update my instruments with the new versions from the library. Use a single instrument prototype for all parts with the same name; this allows the envelope editor to edit all the parts by changing a single sound prototype."

	| unloadPostfix myInstruments name displaysAsUnloaded isUnloaded |
	unloadPostfix _ '(out)'.
	myInstruments _ Dictionary new.
	1 to: instrumentSelector size do: [:i |
		name _ (instrumentSelector at: i) contents.
		displaysAsUnloaded _ name endsWith: unloadPostfix.
		displaysAsUnloaded ifTrue: [
			name _ name copyFrom: 1 to: name size - unloadPostfix size].
		(myInstruments includesKey: name) ifFalse: [
			myInstruments at: name put:
				(name = 'clink'
					ifTrue: [
						(SampledSound
							samples: SampledSound coffeeCupClink
							samplingRate: 11025) copy]
					ifFalse: [
						(AbstractSound
							soundNamed: name
							ifAbsent: [
								(instrumentSelector at: i) contentsClipped: 'default'.
								FMSound default]) copy])].
		scorePlayer instrumentForTrack: i put: (myInstruments at: name).

		"update loaded/unloaded status in instrumentSelector if necessary"
		isUnloaded _ (myInstruments at: name) isKindOf: UnloadedSound.
		(displaysAsUnloaded and: [isUnloaded not])
			ifTrue: [(instrumentSelector at: i) contentsClipped: name].
		(displaysAsUnloaded not and: [isUnloaded])
			ifTrue: [(instrumentSelector at: i) contentsClipped: name, unloadPostfix]].
