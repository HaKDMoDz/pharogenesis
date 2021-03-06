duplicateArrayElementsForLeadingCharShift
"
	self duplicateArrayElementsForLeadingCharShift
"
	| array font |
	self allInstances do: [:s |
		s emphasis = 0 ifTrue: [
			array := s fontArray.
			2 to: (4 min: array size) do: [:i |
				font := array at: i.
				s addNewFont: font at: ((i - 1) << 2) + 1.
			].
		] ifFalse: [
			s reset
		].
	].
