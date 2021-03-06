asXML
	"This method is just used as an example for #testMethodHighlighting."
	
	| writer |
	^String streamContents:[:s|
		writer := nil.
		writer xmlDeclaration: '1.0'.
		writer startTag: 'recording'; endTag.
			writer tag: 'creator' pcData: creator.
			writer tag: 'timestamp' pcData: timeStamp.
			writer tag: 'duration' pcData: duration.
			writer startTag: 'tracks'; endTag.
				tracks do:[:tdata|
					writer startTag: 'track'; attribute: 'type' value: tdata value; endTag.
					writer pcData: tdata key.
					writer endTag: 'track'.
				].
			writer endTag: 'tracks'.
		writer endTag: 'recording'.
	].
