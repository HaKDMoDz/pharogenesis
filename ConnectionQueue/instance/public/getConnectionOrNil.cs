getConnectionOrNil
	"Return a connected socket, or nil if no connection has been established."

	| result |
	accessSema critical: [
		connections isEmpty
			ifTrue: [result := nil]
			ifFalse: [
				result := connections removeFirst.
				((result isValid) and: [result isConnected]) ifFalse: [  "stale connection"
					result destroy.
					result := nil]]].
	^ result
