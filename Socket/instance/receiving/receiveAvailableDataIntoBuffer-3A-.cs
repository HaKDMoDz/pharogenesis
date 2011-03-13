receiveAvailableDataIntoBuffer: buffer
	"Receive all available data (if any). Do not wait."
 
	| bytesRead |
	bytesRead _ self receiveAvailableDataInto: buffer.
	^buffer copyFrom: 1 to: bytesRead