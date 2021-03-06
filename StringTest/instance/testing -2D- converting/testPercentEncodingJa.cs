testPercentEncodingJa
	| leading hiraA hiraO hiraAO encodedHiraA encodedHiraO encodedHiraAO |

    "Make Japanese String from unicode. see http://www.unicode.org/charts/PDF/U3040.pdf"
     leading := JapaneseEnvironment leadingChar.
	hiraA := (Character leadingChar: leading code: 16r3042) asString.  "HIRAGANA LETTER A"
	hiraO := (Character leadingChar: leading code: 16r304A) asString.  "HIRAGANA LETTER O"
	hiraAO := hiraA, hiraO.

	"Percent Encoded Japanese String"
	encodedHiraA := hiraA encodeForHTTP.
	self assert: encodedHiraA = '%E3%81%82'.
	encodedHiraO := hiraO encodeForHTTP.
	self assert: encodedHiraO = '%E3%81%8A'.
	encodedHiraAO := hiraAO encodeForHTTP.
	self assert: encodedHiraAO =  '%E3%81%82%E3%81%8A'.

     "without percent encoded string"
	self assert: '' unescapePercents = ''.
	self assert: 'abc' unescapePercents = 'abc'.	"latin1 character"
	self assert: hiraAO unescapePercents = hiraAO.  "multibyte character"

	"encoded latin1 string"
	self assert: '%61' unescapePercents = 'a'.
	self assert: '%61%62%63' unescapePercents = 'abc'.

	"encoded multibyte string"
	Locale currentPlatform: (Locale isoLanguage: 'ja') during: [ 
		self assert: encodedHiraA unescapePercents = hiraA.
		self assert: encodedHiraAO unescapePercents = hiraAO].

	"mixed string"
	Locale currentPlatform: (Locale isoLanguage: 'ja') during: [ 
		self assert: (encodedHiraAO,'a') unescapePercents = (hiraAO, 'a').
		self assert: ('a', encodedHiraA) unescapePercents = ('a', hiraA).
		self assert: ('a', encodedHiraA, 'b')  unescapePercents = ('a', hiraA, 'b').
		self assert: ('a', encodedHiraA, 'b', encodedHiraO) unescapePercents = ('a', hiraA, 'b', hiraO).
		self assert: (encodedHiraA, encodedHiraO, 'b', encodedHiraA) unescapePercents = (hiraA, hiraO, 'b', hiraA)].


	"for Seaside"
	Locale currentPlatform: (Locale isoLanguage: 'ja') during: [ 
		self assert: (encodedHiraA, '+', encodedHiraO) unescapePercents = (hiraA, ' ', hiraO)].

