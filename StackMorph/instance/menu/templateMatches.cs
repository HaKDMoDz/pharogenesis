templateMatches
	| template docks keys bkg |
	"Current card is the template.  Only search cards in this background. Look at cards directly (not allText). Key must be found in the same field as in the template.  HyperCard style (multiple starts of words).  
	Put results in a list, outside the stack."

	template _ self currentCard.
	template commitCardPlayerData.
	docks _ template class variableDocks.
	(keys _ template asKeys) ifNil: [^ #()]. "nothing to match against"
	bkg _ self currentPage.
	^ self privateCards select: [:cardPlayer | 
		(((cardPlayer == template) not) and: [cardPlayer costume == bkg]) 
			and: [cardPlayer match: keys fields: docks]].
