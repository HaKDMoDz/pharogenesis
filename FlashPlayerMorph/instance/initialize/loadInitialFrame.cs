loadInitialFrame
	"Note: Must only be sent to a player if not in streaming mode"
	self isStreaming ifTrue:[^self].
	super loadInitialFrame.
	activationKeys _ self collectActivationKeys: maxFrames.
	activeMorphs _ SortedCollection new: 50.
	activeMorphs sortBlock:[:m1 :m2| m1 depth > m2 depth].
	activeMorphs addAll: activationKeys first.