initWithContents: anObject prior: priorMorph forList: hostList

	container _ hostList.
	complexContents _ anObject.
	self initWithContents: anObject asString font: nil emphasis: nil.
	indentLevel _ 0.
	isExpanded _ false.
 	nextSibling _ firstChild _ nil.
	priorMorph ifNotNil: [
		priorMorph nextSibling: self.
	].
