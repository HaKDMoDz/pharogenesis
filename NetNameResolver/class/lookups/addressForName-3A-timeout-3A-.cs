addressForName: hostName timeout: secs
	"Look up the given host name and return its address. Return nil if the address is not found in the given number of seconds."
	"NetNameResolver addressForName: 'create.ucsb.edu' timeout: 30"
	"NetNameResolver addressForName: '100000jobs.de' timeout: 30"
	"NetNameResolver addressForName: '1.7.6.4' timeout: 30"
	"NetNameResolver addressForName: '' timeout: 30 (This seems to return nil?)"

	| deadline ready success result |

	"check if this is a valid numeric host address (e.g. 1.2.3.4)"
	result _ self addressFromString: hostName.
	result isNil ifFalse: [^result].

	"Look up a host name, including ones that start with a digit (e.g. 100000jobs.de or squeak.org)"
	deadline _ Time millisecondClockValue + (secs * 1000).
	ready _ self waitForResolverReadyUntil: deadline.
	ready ifFalse: [^ nil].

	self primStartLookupOfName: hostName.
	success _ self waitForCompletionUntil: deadline.
	success
		ifTrue: [^ self primNameLookupResult]
		ifFalse: [^ nil].
