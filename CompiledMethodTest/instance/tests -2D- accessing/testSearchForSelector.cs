testSearchForSelector
	|  method cls |

	method := (self class)>>#returnTrue.
	self assert: (method searchForSelector = #returnTrue).

	"now make an orphaned method. we want to get nil as the selector"	
	
	Smalltalk removeClassNamed: #TUTU.

	cls := Object subclass: #TUTU
		instanceVariableNames: ''
		classVariableNames: ''
		poolDictionaries: ''
		category: 'KernelTests-Methods'.
	cls compile: 'foo ^ 10'.
	method := cls >> #foo.
	Smalltalk removeClassNamed: #TUTU.
	
	self assert: method searchForSelector = nil. 
