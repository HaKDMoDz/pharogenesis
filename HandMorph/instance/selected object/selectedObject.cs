selectedObject
	"answer the selected object for the hand or nil is none"
	| halo |
	halo := self halo.
	halo isNil
		ifTrue: [^ nil].
	^ halo target renderedMorph