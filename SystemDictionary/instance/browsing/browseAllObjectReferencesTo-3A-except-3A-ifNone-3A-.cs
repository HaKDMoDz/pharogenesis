browseAllObjectReferencesTo: anObject except: objectsToExclude ifNone: aBlock
	"Bring up a list inspector on the objects that point to anObject.
	If there are none, then evaluate aBlock on anObject.  "

	| aList shortName |
	aList _ Smalltalk pointersTo: anObject except: objectsToExclude.
	aList size > 0 ifFalse: [^ aBlock value: anObject].
	shortName _ (anObject name ifNil: [anObject printString]) contractTo: 20.
	OrderedCollectionInspector openOn: aList withEvalPane: false
		withLabel: 'Objects pointing to ', shortName.