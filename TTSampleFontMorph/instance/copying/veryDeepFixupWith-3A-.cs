veryDeepFixupWith: deepCopier
	"If fields were weakly copied, fix them here. If they were in the 
	tree being copied, fix them up, otherwise point to the originals!!"

	super veryDeepFixupWith: deepCopier.
	font _ deepCopier references at: font ifAbsent: [font]