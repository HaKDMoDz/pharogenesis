nextComment
	"we've seen < and the next is a !.  read until the whole comment is done"
	"this isn't perfectly correct--for instance <!--> is considered a proper comment--but it should do for now.  It also picks up <!DOCTYPE...> tags"
	| source c hyphens |
	
	self nextChar.   "swallow the $!"
	source _ WriteStream on: String new.
	source nextPutAll: '<!'.
	
	self peekChar = $- ifFalse: [ 
		"this case is wierd.  go until we find a > at all and pray it's the correct end-of-'comment'"
		[	self atEnd or: [ self peekChar = $> ] 
		] whileFalse: [
			c _ self nextChar.
			source nextPut: c 
		].
		self atEnd ifFalse: [ source nextPut: self nextChar ].
		^HtmlComment forSource: source contents ].
	
	hyphens _ 0.

	[ 	c _ self nextChar.
		c = nil or: [
			source nextPut: c.
			(hyphens >=2 and: [ c = $> ])]
	] whileFalse: [
		c = $- ifTrue: [ hyphens _ hyphens + 1 ] ifFalse: [ hyphens _ 0 ]
	].
		
	^HtmlComment forSource: source contents.
