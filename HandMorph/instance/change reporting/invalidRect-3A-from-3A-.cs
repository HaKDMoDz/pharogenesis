invalidRect: damageRect from: aMorph
	"Note that a change has occurred and record the given damage rectangle relative to the origin this hand's cache."
	hasChanged _ true.
	aMorph == self ifTrue:[^self].
	damageRecorder recordInvalidRect: damageRect.
