absorbClass: className from: otherChangeSet
	"Absorb into the receiver all the changes found in the class in the other change set.
	*** Classes renamed in otherChangeSet may have problems"

	| cls |
	(self changeRecorderFor: className)
			assimilateAllChangesIn: (otherChangeSet changeRecorderFor: className).

	(cls := Smalltalk classNamed: className) ifNotNil:
		[self absorbStructureOfClass: cls from: otherChangeSet].
