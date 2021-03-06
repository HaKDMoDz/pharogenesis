position: updateStrm atVersion: version
	"Set the stream to the end of the last line of updates names for this version.  Usually the end of the file.  We will add a new update name.   Return the contents of the rest of the file."

	| char foundIt where data |
	updateStrm reset; ascii.
	foundIt := false.
	[char := updateStrm next.
	 updateStrm atEnd] whileFalse: [
		(char == Character cr or: [char == Character lf]) ifTrue: [
			updateStrm peek == $# ifTrue: [
				foundIt ifTrue: ["Next section"
					where := updateStrm position.
					data := updateStrm upTo: (255 asCharacter).
					updateStrm position: where.
					^ data].	"won't be found -- copy all the way to the end"
				updateStrm next.
				(updateStrm nextMatchAll: version) ifTrue: [
					(updateStrm atEnd or: [(updateStrm peek = Character cr) | 
						(updateStrm peek = Character lf)]) ifTrue: [
							foundIt := true
					]]]]].
	foundIt ifTrue: [
		updateStrm setToEnd.
		^ ''].
	self error: 'The current version does not have a section in the Updates file'.
