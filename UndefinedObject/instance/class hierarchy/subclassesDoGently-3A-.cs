subclassesDoGently: aBlock
	"Evaluate aBlock with all subclasses of nil.  Others are not direct subclasses of Class."

	^ Class subclassesDoGently: [:cl | 
			cl isMeta ifTrue: [aBlock value: cl soleInstance]].