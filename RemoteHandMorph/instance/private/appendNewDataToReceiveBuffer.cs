appendNewDataToReceiveBuffer
	"Append all available raw data to my receive buffer. Assume that my socket is not nil."

	| newData tempBuf bytesRead |
	socket dataAvailable ifTrue: [
		"get all the data currently available"
		newData _ WriteStream on: (String new: receiveBuffer size + 1000).
		newData nextPutAll: receiveBuffer.
		tempBuf _ String new: 1000.
		[socket dataAvailable] whileTrue: [
			bytesRead _ socket receiveDataInto: tempBuf.
			1 to: bytesRead do: [:i | newData nextPut: (tempBuf at: i)]].
		receiveBuffer _ newData contents].
