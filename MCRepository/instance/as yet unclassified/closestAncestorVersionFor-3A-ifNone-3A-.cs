closestAncestorVersionFor: anAncestry ifNone: errorBlock
	anAncestry breadthFirstAncestorsDo:
		[:ancestorInfo |
		(self versionWithInfo: ancestorInfo) ifNotNilDo: [:v | ^ v]].
	^ errorBlock value