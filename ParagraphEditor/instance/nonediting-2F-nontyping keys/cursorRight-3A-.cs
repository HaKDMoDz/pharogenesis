cursorRight: characterStream 
	"Private - Move cursor right one character if nothing selected, 
	otherwise move cursor to end of selection. If the shift key is down, 
	start selecting characters or extending already selected characters. 
	Don't allow cursor past end of text"

	self closeTypeIn: characterStream.
	self
		moveCursor: [:position | position + 1]
		forward: true
		specialBlock:[:position | self nextWord: position].
	^ true