fromString: aString
	"Answer an instance of created from a string with format mm.dd.yyyy."

	^ self readFrom: aString readStream.