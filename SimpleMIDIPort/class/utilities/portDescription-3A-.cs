portDescription: portNum
	"Answer a string indicating the directionality of the given MIDI port."
	"(0 to: SimpleMIDIPort primPortCount - 1) collect:
		[:i | SimpleMIDIPort portDescription: i]"

	| portName dir |
	portName _ (self primPortNameOf: portNum) convertFromSystemString.
	dir _ self primPortDirectionalityOf: portNum.
	dir = 1 ifTrue: [^ portName, ' (in)'].
	dir = 2 ifTrue: [^ portName, ' (out)'].
	dir = 3 ifTrue: [^ portName, ' (in/out)'].
	^ self error: 'unknown MIDI port directionality'
