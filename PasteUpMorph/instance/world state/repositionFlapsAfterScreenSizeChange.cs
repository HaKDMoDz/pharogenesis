repositionFlapsAfterScreenSizeChange
	"Reposition flaps after screen size change"

	(Flaps globalFlapTabsIfAny, ActiveWorld localFlapTabs) do:
		[:aFlapTab |
			aFlapTab applyEdgeFractionWithin: self bounds].
	Flaps doAutomaticLayoutOfFlapsIfAppropriate