displayTextHtmlPage: newSource
	"HTML page--format it"
	| formatter bgimageUrl bgimageDoc bgimage |
	currentUrl _ newSource url.
	pageSource _ newSource content isoToSqueak.
	self status: 'parsing...'.
	document _ (HtmlParser parse: (ReadStream on: pageSource)).
	self status: 'laying out...'.
	formatter _ HtmlFormatter preferredFormatterClass new.
	formatter browser: self.
	formatter baseUrl: currentUrl.
	document addToFormatter: formatter.

	formattedPage _ formatter text.
	(bgimageUrl _ document body background)
		ifNotNil:
			[bgimageDoc _ (bgimageUrl asUrlRelativeTo: currentUrl) retrieveContents.
			[bgimage _ ImageReadWriter formFromStream: bgimageDoc contentStream binary]
				ifError: [:err :rcvr | "ignore" bgimage _ nil]].
	bgimage
		ifNotNil: [backgroundColor _ bgimage]
		ifNil: [backgroundColor _ Color fromString: document body bgcolor].
	currentUrl fragment
		ifNil: [ currentAnchorLocation _ nil ]
		ifNotNil: [ currentAnchorLocation _
				formatter anchorLocations 
					at: currentUrl fragment asLowercase
					ifAbsent: [ nil ] ].

	self startDownloadingMorphState: (formatter incompleteMorphs).

	self changeAll: 	#(currentUrl relabel hasLint lint backgroundColor formattedPage formattedPageSelection).
	^true