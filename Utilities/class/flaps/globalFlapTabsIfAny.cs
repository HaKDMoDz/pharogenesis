globalFlapTabsIfAny
	"Answer a list of the global flap tabs, but it they don't exist, just answer an empty list"
	^ FlapTabs copy ifNil: [Array new]