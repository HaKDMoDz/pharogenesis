children
	"Answer a list of all the subprojects of the receiver"
	
	| children |
	children _ OrderedCollection new.
	Project allProjects do: [ :p | 
		(self == p parent and: [self ~~ p]) ifTrue:
			[ children add: p ]].
	^ children

"
Project topProject children
"