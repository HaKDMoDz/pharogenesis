primNextFillEntryInto: fillEntry
	"Store the next fill entry of the active edge table in fillEntry.
	Return false if there is no such entry, true otherwise"
	<primitive: 'gePrimitiveNextFillEntry'>
	Debug ifTrue:[^BalloonEnginePlugin doPrimitive: 'gePrimitiveNextFillEntry'].
	^self primitiveFailed