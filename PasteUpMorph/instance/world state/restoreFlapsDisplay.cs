restoreFlapsDisplay
	"Restore the display of flaps"

	(Flaps sharedFlapsAllowed and: [Project current flapsSuppressed not]) ifTrue:
		[Flaps globalFlapTabs do:
			[:aFlapTab | aFlapTab adaptToWorld]].
	self localFlapTabs do:
			[:aFlapTab | aFlapTab adaptToWorld].
	self assureFlapTabsFitOnScreen.
	self bringTopmostsToFront.