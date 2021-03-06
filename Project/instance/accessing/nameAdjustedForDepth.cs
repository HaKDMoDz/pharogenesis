nameAdjustedForDepth
	"Answer the name of the project, prepended with spaces reflecting the receiver's depth from the top project"
	"	Project current nameAdjustedForDepth	"

	| stream |
	stream := String new writeStream.
	self depth timesRepeat: 
	  [2 timesRepeat: [stream nextPut: $ ]].
	stream nextPutAll: self name.
	^ stream contents