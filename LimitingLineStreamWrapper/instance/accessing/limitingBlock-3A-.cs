limitingBlock: aBlock
	"The limitingBlock is evaluated with a line to check if this line terminates the stream"

	limitingBlock _ aBlock fixTemps.
	self updatePosition