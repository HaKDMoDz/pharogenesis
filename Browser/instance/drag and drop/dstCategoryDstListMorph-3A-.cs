dstCategoryDstListMorph: dstListMorph
	^(dstListMorph getListSelector == #systemCategoryList)
		ifTrue: [dstListMorph potentialDropItem ]
		ifFalse: [self selectedSystemCategoryName]