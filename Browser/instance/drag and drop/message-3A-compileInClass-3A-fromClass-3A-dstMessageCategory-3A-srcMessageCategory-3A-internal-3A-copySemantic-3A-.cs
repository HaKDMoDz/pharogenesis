message: messageSel compileInClass: dstClassOrMeta fromClass: srcClassOrMeta dstMessageCategory: dstMessageCategorySel srcMessageCategory: srcMessageCategorySel internal: internal copySemantic: copyFlag 
	| source messageCategorySel tm success oldOrNoMethod newMethod |
	source _ srcClassOrMeta sourceCodeAt: messageSel.
	messageCategorySel _ dstMessageCategorySel ifNil: [srcMessageCategorySel].
	self selectClass: dstClassOrMeta theNonMetaClass.
	(self messageCategoryList includes: messageCategorySel)
		ifFalse: ["create message category"
			self classOrMetaClassOrganizer addCategory: messageCategorySel].
	self selectMessageCategoryNamed: messageCategorySel.
	tm _ self codeTextMorph.
	tm setText: source.
	tm setSelection: (0 to: 0).
	tm hasUnacceptedEdits: true.
	oldOrNoMethod _ srcClassOrMeta compiledMethodAt: messageSel ifAbsent: [].
	tm accept.
	"compilation successful?"
	newMethod _ dstClassOrMeta compiledMethodAt: messageSel ifAbsent: [].
	success _ newMethod ~~ nil & (newMethod ~~ oldOrNoMethod).
	"	success ifFalse: [TransferMorph allInstances do: [:e | e delete]].            
	 "
	success
		ifTrue: 
			[copyFlag not ifTrue: ["remove old method in move semantic if new exists"
		srcClassOrMeta removeSelector: messageSel].internal
				ifTrue: [self selectClass: srcClassOrMeta]
				ifFalse: [self selectClass: dstClassOrMeta].
			self setSelector: messageSel].
	^ success