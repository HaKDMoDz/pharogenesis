readFrom: aBinaryStream
	"Read AIFF data from the given binary stream."
	"Details: An AIFF file consists of a header (FORM chunk) followed by a sequence of tagged data chunks. Each chunk starts with a header consisting of a four-byte tag (a string) and a four byte size. These eight bytes of chunk header are not included in the chunk size. For each chunk, the readChunk:size: method consumes chunkSize bytes of the input stream, parsing recognized chunks or skipping unrecognized ones. If chunkSize is odd, it will be followed by a padding byte. Chunks may occur in any order."

	| sz end chunkType chunkSize p |
	in _ aBinaryStream.

	"read FORM chunk"
	(in next: 4) asString = 'FORM' ifFalse: [^ self error: 'not an AIFF file'].
	sz _ in nextNumber: 4.
	end _ in position + sz.
	fileType _ (in next: 4) asString.

	[in atEnd not and: [in position < end]] whileTrue: [
		chunkType _ (in next: 4) asString.
		chunkSize _ in nextNumber: 4.
		p _ in position.
		self readChunk: chunkType size: chunkSize.
		(in position = (p + chunkSize))
			ifFalse: [self error: 'chunk size mismatch; bad AIFF file?'].
		chunkSize odd ifTrue: [in skip: 1]].  "skip padding byte"
