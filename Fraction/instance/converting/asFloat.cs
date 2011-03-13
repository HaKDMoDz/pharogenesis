asFloat
	"Answer a Float that closely approximates the value of the receiver.
	This implementation will answer the closest floating point number to
	the receiver.
	It uses the IEEE 754 round to nearest even mode
	(can happen in case denominator is a power of two)"
	
	| a b q r exponent floatExponent n ha hb hq q1 |
	a := numerator abs.
	b := denominator abs.
	ha := a highBit.
	hb := b highBit.
	
	"If both numerator and denominator are represented exactly in floating point number,
	then fastest thing to do is to use hardwired float division"
	(ha < 54 and: [hb < 54]) ifTrue: [^numerator asFloat / denominator asFloat].
	
	"Try and obtain a mantissa with 54 bits.
	First guess is rough, we might get one more bit or one less"
	exponent := ha - hb - 54.
	exponent > 0
		ifTrue: [b := b bitShift: exponent]
		ifFalse: [a := a bitShift: exponent negated].
	q := a quo: b.
	r := a - (q * b).
	hq := q highBit.
	
	"check for gradual underflow, in which case we should use less bits"
	floatExponent := exponent + hq - 1.
	n := floatExponent > -1023
		ifTrue: [54]
		ifFalse: [54 + floatExponent + 1022].
	
	hq > n
		ifTrue: [exponent := exponent + hq - n.
			r := (q bitAnd: (1 bitShift: n - hq) - 1) * b + r.
			q := q bitShift: n - hq].
	hq < n
		ifTrue: [exponent := exponent + hq - n.
			q1 := (r bitShift: n - hq) quo: b.
			q := (q bitShift: n - hq) bitAnd: q1.
			r := r - (q1 * b)].
		
	"check if we should round upward.
	The case of exact half (q bitAnd: 1) isZero not & (r isZero)
	will be handled by Integer>>asFloat"
	((q bitAnd: 1) isZero or: [r isZero])
		ifFalse: [q := q + 1].
		
	^ (self positive
		ifTrue: [q asFloat]
		ifFalse: [q asFloat negated])
		timesTwoPower: exponent