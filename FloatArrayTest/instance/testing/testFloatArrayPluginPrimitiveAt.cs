testFloatArrayPluginPrimitiveAt
	"if FloatArrayPlugin primitive are not here, this test is dumb.
	Otherwise, it will compare primitive and #fromIEEE32Bit:"
	
	#(
		"regular numbers no truncation or rounding"
		2r0.0 2r1.0 2r1.1 2r1.00000000000000000000001
		2r1.0e-10 2r1.1e-10 2r1.00000000000000000000001e-10
		2r1.0e10 2r1.1e10 2r1.00000000000000000000001e10
		
		"smallest float32 before gradual underflow"
		2r1.0e-126
		
		"biggest float32"
		2r1.11111111111111111111111e127
		
		"overflow"
		2r1.11111111111111111111111e128
		
		"gradual underflow"
		2r0.11111111111111111111111e-126
		2r0.00000000000000000000001e-126
		
		"with rounding mode : tests on 25 bits"
		
		2r1.0000000000000000000000001
		2r1.0000000000000000000000010
		2r1.0000000000000000000000011
		2r1.0000000000000000000000100
		2r1.0000000000000000000000101
		2r1.0000000000000000000000110
		2r1.0000000000000000000000111
		2r1.1111111111111111111111001
		2r1.1111111111111111111111010
		2r1.1111111111111111111111011
		2r1.1111111111111111111111101
		2r1.1111111111111111111111110
		2r1.1111111111111111111111111
		
		"overflow"
		2r1.1111111111111111111111110e127
		
		"gradual underflow"
		2r0.1111111111111111111111111e-126
		2r0.1111111111111111111111110e-126
		2r0.1111111111111111111111101e-126
		2r0.1111111111111111111111011e-126
		2r0.1111111111111111111111010e-126
		2r0.1111111111111111111111001e-126
		2r0.0000000000000000000000111e-126
		2r0.0000000000000000000000110e-126
		2r0.0000000000000000000000101e-126
		2r0.0000000000000000000000011e-126
		2r0.0000000000000000000000010e-126
		2r0.0000000000000000000000001e-126
		2r0.0000000000000000000000010000000000000000000000000001e-126
		) do: [:e |
			self assert: ((FloatArray with: e) at: 1) = (Float fromIEEE32Bit: ((FloatArray with: e) basicAt: 1)).
			self assert: ((FloatArray with: e negated) at: 1) = (Float fromIEEE32Bit: ((FloatArray with: e negated) basicAt: 1))].
		
	"special cases"
	(Array with: Float infinity with: Float infinity negated with: Float negativeZero)
		do: [:e | self assert: ((FloatArray with: e) at: 1) = (Float fromIEEE32Bit: ((FloatArray with: e) basicAt: 1))].
		
	"Cannot compare NaN"
	(Array with: Float nan)
		do: [:e | self assert: (Float fromIEEE32Bit: ((FloatArray with: e) basicAt: 1)) isNaN].