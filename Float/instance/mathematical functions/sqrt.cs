sqrt
	"Answer the square root of the receiver. 
	 Optional. See Object documentation whatIsAPrimitive."
	| exp guess eps delta |
	<primitive: 55>
	#Numeric.
	"Changed 200/01/19 For ANSI <number> support."
	"Newton-Raphson"
	self <= 0.0
		ifTrue: [self = 0.0
				ifTrue: [^ 0.0]
				ifFalse: ["v Chg"
					^ FloatingPointException signal: 'undefined if less than zero.']].
	"first guess is half the exponent"
	exp := self exponent // 2.
	guess := self timesTwoPower: 0 - exp.
	"get eps value"
	eps := guess * Epsilon.
	eps := eps * eps.
	delta := self - (guess * guess) / (guess * 2.0).
	[delta * delta > eps]
		whileTrue: 
			[guess := guess + delta.
			delta := self - (guess * guess) / (guess * 2.0)].
	^ guess