stepToNextScanLineAt: yValue in: edgeTableEntry
	"Compute the next x value for the scan line at yValue.
	This message is sent during incremental updates. 
	The yValue parameter is passed in here for edges
	that have more complicated computations,"
	| x |
	x _ edgeTableEntry xValue + xIncrement.
	error _ error + errorAdjUp.
	error > 0 ifTrue:[
		x _ x + xDirection.
		error _ error - errorAdjDown.
	].
	edgeTableEntry xValue: x.