waitForSocks4ConnectionReply

	| response |
	response _ self waitForReply: 8 for: self defaultTimeOutDuration.

	(response at: 2) = self requestGrantedCode
		ifFalse: [^self socksError: 'Connection failed: ' , (response at: 2) printString].