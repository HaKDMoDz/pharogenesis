addCustomMenuItems: aCustomMenu hand: aHandMorph 
	super addCustomMenuItems: aCustomMenu hand: aHandMorph.
	aCustomMenu addUpdating: #getDragAndDropState action: #toggleDragAndDropState.
