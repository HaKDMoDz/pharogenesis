testHash
	"test that different instances of the same TextFontChange hash to the 
	same value"
	| hashes hash |
	hashes _ OrderedCollection new.
	1
		to: 100
		do: [:i | hashes add: TextFontChange defaultFontChange hash].
	hash _ hashes at: 1.
	2
		to: 100
		do: [:i | self assert: (hashes at: i)
					= hash]