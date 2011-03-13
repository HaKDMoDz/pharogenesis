versionReaderForFileNamed: aString do: aBlock
	^ self
		readStreamForFileNamed: aString
		do: [:s |
			(MCVersionReader readerClassForFileNamed: aString) ifNotNilDo:
				[:class | aBlock value: (class on: s fileName: aString)]]
