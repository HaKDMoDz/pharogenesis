composeLinesFrom: start to: stop delta: delta into: lineColl priorLines: priorLines
	atY: startingY
	"While the section from start to stop has changed, composition may ripple all the way to the end of the text.  However in a rectangular container, if we ever find a line beginning with the same character as before (ie corresponding to delta in the old lines), then we can just copy the old lines from there to the end of the container, with adjusted indices and y-values"

	| charIndex lineY lineHeight scanner line row firstLine lineHeightGuess saveCharIndex hitCR maybeSlide sliding bottom priorIndex priorLine |
	charIndex _ start.
	lines _ lineColl.
	lineY _ startingY.
	lineHeightGuess _ textStyle lineGrid.
	maxRightX _ container left.
	maybeSlide _ stop < text size and: [container isMemberOf: Rectangle].
	sliding _ false.
	priorIndex _ 1.
	bottom _ container bottom.
	scanner _ CompositionScanner new text: text textStyle: textStyle.
	firstLine _ true.
	[charIndex <= text size and: [(lineY + lineHeightGuess) <= bottom]]
		whileTrue:
		[sliding
			ifTrue:
			["Having detected the end of rippling recoposition, we are only sliding old lines"
			priorIndex < priorLines size
				ifTrue: ["Adjust and re-use previously composed line"
						priorIndex _ priorIndex + 1.
						priorLine _ (priorLines at: priorIndex)
									slideIndexBy: delta andMoveTopTo: lineY.
						lineColl addLast: priorLine.
						lineY _ priorLine bottom.
						charIndex _ priorLine last + 1]
				ifFalse: ["There are no more priorLines to slide."
						sliding _ maybeSlide _ false]]
			ifFalse:
			[lineHeight _ lineHeightGuess.
			saveCharIndex _ charIndex.
			hitCR _ false.
			row _ container rectanglesAt: lineY height: lineHeight.
			1 to: row size do:
				[:i | (charIndex <= text size and: [hitCR not]) ifTrue:
						[line _ scanner composeFrom: charIndex inRectangle: (row at: i)
								firstLine: firstLine leftSide: i=1 rightSide: i=row size.
					lines addLast: line.
					(text at: line last) = Character cr ifTrue: [hitCR _ true].
					lineHeight _ lineHeight max: line lineHeight.  "includes font changes"
					charIndex _ line last + 1]].
			row size >= 1 ifTrue:
			[lineY _ lineY + lineHeight.
			lineY > bottom
				ifTrue: ["Oops -- the line is really too high to fit -- back out"
						charIndex _ saveCharIndex.
						row do: [:r | lines removeLast]]
				ifFalse: ["It's OK -- the line still fits."
						maxRightX _ maxRightX max: scanner rightX.
						1 to: row size - 1 do:  "Adjust heights across row if necess"
							[:i | (lines at: lines size - row size + i)
									lineHeight: lines last lineHeight
									baseline: lines last baseline].
						charIndex > text size ifTrue:
							["end of text"
							hitCR ifTrue:
								["If text ends with CR, add a null line at the end"
								((lineY + lineHeightGuess) <= container bottom) ifTrue:
									[row _ container rectanglesAt: lineY height: lineHeightGuess.
									row size > 0 ifTrue:
										[line _ (TextLine start: charIndex stop: charIndex-1
											internalSpaces: 0 paddingWidth: 0)
										rectangle: row first;
										lineHeight: lineHeightGuess baseline: textStyle baseline.
										lines addLast: line]]].
							lines _ lines asArray.
							^ maxRightX].
						firstLine _ false]]
				ifFalse:
				[lineY _ lineY + lineHeight].
			(maybeSlide and: [charIndex > stop]) ifTrue:
				["Check whether we are now in sync with previously composed lines"
				 [priorIndex < priorLines size
					and: [(priorLines at: priorIndex) first < (charIndex - delta)]]
						whileTrue: [priorIndex _ priorIndex + 1].
				(priorLines at: priorIndex) first = (charIndex - delta)
					ifTrue: ["Yes -- next line will have same start as prior line."
							priorIndex _ priorIndex - 1.
							maybeSlide _ false.
							sliding _ true]
					ifFalse: [priorIndex = priorLines size ifTrue:
								["Weve reached the end of priorLines,
								so no use to keep looking for lines to slide."
								maybeSlide _ false]]]]].
	firstLine ifTrue:
		["No space in container or empty text"
		line _ (TextLine start: start stop: start-1
						internalSpaces: 0 paddingWidth: 0)
				rectangle: (container topLeft extent: 0@lineHeightGuess);
				lineHeight: lineHeightGuess baseline: textStyle baseline.
		lines _ Array with: line].
	"end of container"
	lines _ lines asArray.
	^ maxRightX