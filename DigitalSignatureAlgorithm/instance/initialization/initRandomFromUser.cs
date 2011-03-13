initRandomFromUser
	"Ask the user to type a long random string and use the result to seed the secure random number generator."

	| s k srcIndex |
	s := UIManager default request: 'Enter a long random string to seed the random generator.'.
	k := LargePositiveInteger new: (s size min: 64).
	srcIndex := 0.
	k digitLength to: 1 by: -1 do: [:i |
		k digitAt: i put: (s at: (srcIndex := srcIndex + 1)) asciiValue].
	k := k + (Random new next * 16r7FFFFFFF) asInteger.  "a few additional bits randomness"
	k highBit > 512 ifTrue: [k := k bitShift: k highBit - 512].
	self initRandom: k.
