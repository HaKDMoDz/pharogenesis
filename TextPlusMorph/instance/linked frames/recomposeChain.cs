recomposeChain
	"Recompose this textMorph and all that follow it."
	self withSuccessorsDo:
		[:m |  m text: text textStyle: textStyle;  "Propagate new style if any"
				releaseParagraphReally;  "Force recomposition"
				fit  "and propagate the change"]
