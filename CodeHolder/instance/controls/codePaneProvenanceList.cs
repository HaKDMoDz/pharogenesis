codePaneProvenanceList
	"Answer a list of the display strings for code provenance."
	
	^(self contentsSymbolQuints select: [:e | e ~= #-]) collect: [:e |
		e fourth]