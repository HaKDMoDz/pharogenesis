testNewLineLeaveSpacesOnOldLine
	"Checks whether the drawing of indented text is really indented..."
	| cb |
	para replaceFrom: 7 to: 7 with: (String with: Character cr) displaying: false.

	cb := para characterBlockForIndex: 8.
	self assert: (para asForm copy: (0@cb top extent: 24@cb height)) isAllWhite