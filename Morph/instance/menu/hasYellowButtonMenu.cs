hasYellowButtonMenu
	"Answer true if I have any items at all for a context (yellow  
	button) menu."
	^ self wantsYellowButtonMenu
			or: [self models anySatisfy: [:each | each hasModelYellowButtonMenuItems]]