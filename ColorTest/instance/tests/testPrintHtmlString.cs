testPrintHtmlString
	"self debug: #testPrintHtmlString"
	
	self shouldnt: [Color white printHtmlString ] raise: Error.
	self assert: Color white printHtmlString = 'FFFFFF'.
	self assert: Color red printHtmlString =  'FF0000'.
	self assert: Color black printHtmlString = '000000'.