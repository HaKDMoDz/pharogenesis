downHeapSingle: anIndex
	"This version is optimized for the case when only one element in the receiver can be at a wrong position. It avoids one comparison at each node when travelling down the heap and checks the heap upwards after the element is at a bottom position. Since the probability for being at the bottom of the heap is much larger than for being somewhere in the middle this version should be faster."
	| value k n j |
	anIndex = 0 ifTrue:[^self].
	n _ tally bitShift: -1.
	k _ anIndex.
	value _ array at: anIndex.
	[k <= n] whileTrue:[
		j _ k + k.
		"use max(j,j+1)"
		(j < tally and:[self sorts: (array at: j+1) before: (array at: j)])
				ifTrue:[	j _ j + 1].
		array at: k put: (array at: j).
		"and try again with j"
		k _ j].
	array at: k put: value.
	self upHeap: k