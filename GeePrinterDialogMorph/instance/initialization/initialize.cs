initialize
	"initialize the state of the receiver"
	super initialize.
	""
	self vResizing: #shrinkWrap;
		 hResizing: #shrinkWrap;
		 layoutInset: 4;
		 useRoundedCorners.
	printSpecs
		ifNil: [printSpecs _ PrintSpecifications defaultSpecs].
	self rebuild 