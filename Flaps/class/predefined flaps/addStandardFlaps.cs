addStandardFlaps
	"Initialize the standard default out-of-box set of global flaps. 
	This method creates them and places them in my class 
	variable #SharedFlapTabs, but does not itself get them 
	displayed. "
	SharedFlapTabs
		ifNil: [SharedFlapTabs := OrderedCollection new].
	SharedFlapTabs add: self newSqueakFlap.
	SharedFlapTabs add: self newSuppliesFlap.
	SharedFlapTabs add: self newToolsFlap.
	SharedFlapTabs add: self newWidgetsFlap.
	SharedFlapTabs add: self newStackToolsFlap.
	SharedFlapTabs add: self newNavigatorFlap.
	SharedFlapTabs add: self newPaintingFlap.
	SharedFlapTabs add: self newObjectsFlap.
	self disableGlobalFlapWithID: 'Stack Tools' translated.
	self disableGlobalFlapWithID: 'Painting' translated.
	self disableGlobalFlapWithID: 'Navigator' translated.
	self disableGlobalFlapWithID: 'Objects' translated.
	^ SharedFlapTabs