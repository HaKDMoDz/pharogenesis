informUserAt: aPoint during: aBlock
	"Add this menu to the Morphic world during the execution of the given block."

	| title w |
	Smalltalk isMorphic ifFalse: [^ self].

	title _ self allMorphs detect: [ :ea | ea hasProperty: #titleString ].
	title _ title submorphs first.
	self visible: false.
	w _ ActiveWorld.
	aBlock value:[:string|
		self visible ifFalse:[
			w addMorph: self centeredNear: aPoint.
			self visible: true].
		title contents: string.
		self setConstrainedPosition: Sensor cursorPoint hangOut: false.
		self changed.
		w displayWorld		 "show myself"
	]. 
	self delete.
	w displayWorld