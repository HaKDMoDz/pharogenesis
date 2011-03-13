browseClassVarRefs: aClass
	"Put up a menu offering all class variable names; if the user chooses one, open up a message-list browser on all methods 
	that refer to the selected class variable"

	| lines labelStream vars allVars index owningClasses |
	lines _ OrderedCollection new.
	allVars _ OrderedCollection new.
	owningClasses _ OrderedCollection new.
	labelStream _ WriteStream on: (String new: 200).
	aClass withAllSuperclasses reverseDo:
		[:class |
		vars _ class classVarNames asSortedCollection.
		vars do:
			[:var |
			labelStream nextPutAll: var; cr.
			allVars add: var.
			owningClasses add: class].
		vars isEmpty ifFalse: [lines add: allVars size]].
	labelStream contents isEmpty ifTrue: [^Beeper beep]. "handle nil superclass better"
	labelStream skip: -1 "cut last CR".
	index _ (UIManager default chooseFrom: (labelStream contents substrings) lines: lines).
	index = 0 ifTrue: [^ self].
	self browseAllCallsOn:
		((owningClasses at: index) classPool associationAt: (allVars at: index))