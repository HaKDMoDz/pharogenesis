log: aString
	Transcript show: aString.
	self logFileStream nextPutAll: aString.
