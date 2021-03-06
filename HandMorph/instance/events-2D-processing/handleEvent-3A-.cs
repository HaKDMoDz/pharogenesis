handleEvent: anEvent
	| evt ofs |
	owner ifNil:[^self].
	evt := anEvent.

	EventStats ifNil:[EventStats := IdentityDictionary new].
	EventStats at: #count put: (EventStats at: #count ifAbsent:[0]) + 1.
	EventStats at: evt type put: (EventStats at: evt type ifAbsent:[0]) + 1.

	evt isWindowEvent ifTrue: [^self].
	evt isMouseOver ifTrue:[^self sendMouseEvent: evt].

ShowEvents == true ifTrue:[
	Display fill: (0@0 extent: 250@120) rule: Form over fillColor: Color white.
	ofs := (owner hands indexOf: self) - 1 * 60.
	evt printString displayAt: (0@ofs) + (evt isKeyboard ifTrue:[0@30] ifFalse:[0@0]).
	self keyboardFocus printString displayAt: (0@ofs)+(0@45).
].
	"Notify listeners"
	self sendListenEvent: evt to: self eventListeners.

	evt isKeyboard ifTrue:[
		self sendListenEvent: evt to: self keyboardListeners.
		self sendKeyboardEvent: evt.
		^self mouseOverHandler processMouseOver: lastMouseEvent].

	evt isDropEvent ifTrue:[
		self sendEvent: evt focus: nil.
		^self mouseOverHandler processMouseOver: lastMouseEvent].

	evt isMouse ifTrue:[
		self sendListenEvent: evt to: self mouseListeners.
		lastMouseEvent := evt].

	"Check for pending drag or double click operations."
	mouseClickState ifNotNil:[
		(mouseClickState handleEvent: evt from: self) ifFalse:[
			"Possibly dispatched #click: or something and will not re-establish otherwise"
			^self mouseOverHandler processMouseOver: lastMouseEvent]].

	evt isMove ifTrue:[
		self position: evt position.
		self sendMouseEvent: evt.
	] ifFalse:[
		"Issue a synthetic move event if we're not at the position of the event"
		(evt position = self position) ifFalse:[self moveToEvent: evt].
		"Drop submorphs on button events"
		(self hasSubmorphs) 
			ifTrue:[self dropMorphs: evt]
			ifFalse:[self sendMouseEvent: evt].
	].
	ShowEvents == true ifTrue:[self mouseFocus printString displayAt: (0@ofs) + (0@15)].
	self mouseOverHandler processMouseOver: lastMouseEvent.
