scroll: amt
	"Move the stamps over"

	start _ start - 1 + amt \\ stamps size + 1.
	self normalize.	"show them"