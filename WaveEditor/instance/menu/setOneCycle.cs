setOneCycle
	"Set the approximate frequency based on a single cycle specified by the user. To use this, first set the loop end, then place the cursor one full cycle before the loop end and invoke this method."

	| len |
	len _ loopEnd - graph cursor.
	len > 0 ifTrue: [
		loopCycles _ 1.
		self loopLength: len].
