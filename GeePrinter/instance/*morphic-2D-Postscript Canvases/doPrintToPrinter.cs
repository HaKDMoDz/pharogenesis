doPrintToPrinter

	"fileName _ ('gee.',Time millisecondClockValue printString,'.eps') asFileName."
	self pageRectangles.	"ensure bounds computed"
	DSCPostscriptCanvasToDisk 
		morphAsPostscript: self 
		rotated: self printSpecs landscapeFlag
		specs: self printSpecs
