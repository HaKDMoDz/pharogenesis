drawString: aString from: firstIndex to: lastIndex in: bounds font: fontOrNil color: c underline: underline underlineColor: uc strikethrough: strikethrough strikethroughColor: sc
	| font portRect endPoint |
	port colorMap: nil.
	portRect := port clipRect.
	port clipByX1: bounds left + origin x 
		y1: bounds top + origin y 
		x2: bounds right + origin x 
		y2: bounds bottom + origin y.
	font := fontOrNil ifNil: [TextStyle defaultFont].
	port combinationRule: Form paint.
	font installOn: port
		foregroundColor: (self shadowColor ifNil:[c]) 
		backgroundColor: Color transparent.
	endPoint := font displayString: aString asString on: port 
		from: firstIndex to: lastIndex at: (bounds topLeft + origin) kern: 0.
	underline ifTrue:[
		font installOn: port
			foregroundColor: (self shadowColor ifNil:[uc]) 
			backgroundColor: Color transparent.
		font displayUnderlineOn: port from: (bounds topLeft + origin + (0@font ascent)) to: endPoint.	
		].
	port clipRect: portRect.