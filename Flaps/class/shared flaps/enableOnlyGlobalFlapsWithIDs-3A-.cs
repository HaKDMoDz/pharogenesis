enableOnlyGlobalFlapsWithIDs: survivorList
	"In the current project, suppress all global flaps other than those with ids in the survivorList"

	self globalFlapTabsIfAny do: [:aFlapTab |
		(survivorList includes: aFlapTab flapID)
			ifTrue:
				[self enableGlobalFlapWithID: aFlapTab flapID]
			ifFalse:
				[self disableGlobalFlapWithID: aFlapTab flapID]].
	ActiveWorld addGlobalFlaps 

	"Flaps enableOnlyGlobalFlapsWithIDs: #('Supplies')"