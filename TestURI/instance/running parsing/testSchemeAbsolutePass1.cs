testSchemeAbsolutePass1
	| uri |
	uri _ URI fromString: 'http://www.squeakland.org'.
	self should: [uri scheme = 'http'].
	self should: [uri isAbsolute].
	self shouldnt: [uri isOpaque].
	self shouldnt: [uri isRelative]