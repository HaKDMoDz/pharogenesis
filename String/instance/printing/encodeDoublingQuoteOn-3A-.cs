encodeDoublingQuoteOn: aStream 
	"Print inside string quotes, doubling inbedded quotes."
	| x |
	aStream print: $'.
	1 to: self size do:
		[:i |
		aStream print: (x _ self at: i).
		x = $' ifTrue: [aStream print: x]].
	aStream print: $'