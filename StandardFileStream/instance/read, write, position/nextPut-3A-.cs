nextPut: char
	"Write the given character to this file."

	rwmode ifFalse: [^ self error: 'Cannot write a read-only file'].
	buffer1 at: 1 put: char.
	self primWrite: fileID from: buffer1 startingAt: 1 count: 1.
	^ char
