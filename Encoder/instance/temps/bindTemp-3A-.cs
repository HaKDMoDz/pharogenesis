bindTemp: name 
	"Declare a temporary; error not if a field or class variable."
	scopeTable at: name ifPresent:[:node|
		"When non-interactive raise the error only if its a duplicate"
		(node isTemp or:[requestor interactive])
			ifTrue:[^self notify:'Name is already defined']
			ifFalse:[Transcript show: '(', name, ' is shadowed)']].
	^self reallyBind: name