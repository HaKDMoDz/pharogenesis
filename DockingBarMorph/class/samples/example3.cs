example3
	" 
	DockingBarMorph example3. 
	World deleteDockingBars.
	"
	(Color lightBlue wheel: 4)
		with: #(#top #bottom #left #right )
		do: [:col :edge | 
			| instance | 
			instance := DockingBarMorph example1.
			instance adhereTo: edge.
			instance color: col.
			instance borderColor: col twiceDarker]