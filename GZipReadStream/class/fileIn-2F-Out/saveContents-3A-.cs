saveContents: fullFileName
	"Save the contents of a gzipped file"
	| zipped buffer unzipped newName |
	newName _ fullFileName copyUpToLast: FileDirectory extensionDelimiter.
	unzipped _ FileStream newFileNamed: newName.
	unzipped binary.
	zipped _ GZipReadStream on: (FileStream readOnlyFileNamed: fullFileName).
	buffer _ ByteArray new: 50000.
	'Extracting ' , fullFileName
		displayProgressAt: Sensor cursorPoint
		from: 0
		to: zipped sourceStream size
		during: 
			[:bar | 
			[zipped atEnd]
				whileFalse: 
					[bar value: zipped sourceStream position.
					unzipped nextPutAll: (zipped nextInto: buffer)].
			zipped close.
			unzipped close].
	^ newName