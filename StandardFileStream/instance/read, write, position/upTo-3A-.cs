upTo: delim 
	"Fast version to speed up nextChunk"
	| pos buffer count |
	pos := self position.
	buffer := self next: 2000.
	(count := buffer indexOf: delim) > 0 ifTrue: 
		["Found the delimiter part way into buffer"
		self position: pos + count.
		^ buffer copyFrom: 1 to: count - 1].
	self atEnd ifTrue:
		["Never found it, and hit end of file"
		^ buffer].
	"Never found it, but there's more..."
	^ buffer , (self upTo: delim)