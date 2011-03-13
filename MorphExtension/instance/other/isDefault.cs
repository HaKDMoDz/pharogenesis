isDefault
	"Return true if the receiver is a default and can be omitted"
	locked == true
		ifTrue: [^ false].
	visible == false
		ifTrue: [^ false].
	sticky == true
		ifTrue: [^ false].
	balloonText isNil
		ifFalse: [^ false].
	balloonTextSelector isNil
		ifFalse: [^ false].
	externalName isNil
		ifFalse: [^ false].
	isPartsDonor == true
		ifTrue: [^ false].
	actorState isNil
		ifFalse: [^ false].
	player isNil
		ifFalse: [^ false].
	eventHandler isNil
		ifFalse: [^ false].
	otherProperties ifNotNil: [otherProperties isEmpty ifFalse: [^ false]].
	^ true