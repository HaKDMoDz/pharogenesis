interrupted
	"Something went wrong - we're about to bring up a debugger. 
	Release some stuff that could be problematic."
	self releaseAllFoci. "or else debugger might not handle clicks"
