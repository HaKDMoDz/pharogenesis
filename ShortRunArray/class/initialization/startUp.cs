startUp
	"Check if the word order has changed from the last save"
	((LastSaveOrder valueAtRun: 1) = 42 and:[(LastSaveOrder lengthAtRun: 1) = 3]) 
		ifTrue:[^self]. "Okay"
	((LastSaveOrder lengthAtRun: 1) = 42 and:[(LastSaveOrder valueAtRun: 1) = 3]) 
		ifTrue:[^self swapRuns]. "Reverse guys"
	^self error:'This must never happen'