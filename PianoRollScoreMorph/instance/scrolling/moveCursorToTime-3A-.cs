moveCursorToTime: scoreTime

	| cursorOffset desiredCursorHeight |
	scorePlayer isPlaying
		ifTrue:
			[cursorOffset _ ((scoreTime - leftEdgeTime) asFloat * timeScale) asInteger.
			(cursorOffset < 0
				or: [cursorOffset > (self width-20)])
				ifTrue:
				[self goToTime: scoreTime - (20/timeScale).
				cursorOffset _ ((scoreTime - leftEdgeTime) asFloat * timeScale) asInteger]]
		ifFalse:
			[self goToTime: scoreTime - (self width//2 / timeScale).
			cursorOffset _ ((scoreTime - leftEdgeTime) asFloat * timeScale) asInteger].

	cursor position: (self left + borderWidth + cursorOffset)@(self top + borderWidth).
	desiredCursorHeight _ self height.
	cursor height ~= desiredCursorHeight ifTrue: [cursor extent: 1@desiredCursorHeight].
