setPointOfView: aVector duration: aDuration
	"Sets the object's position and orientation"

	| positionVector rotationVector |

	((aDuration = rightNow) or: [ aDuration = eachFrame ]) ifTrue: [
		[ WonderlandVerifier VerifyPointOfView: aVector ]
			ifError: [ :msg :rcvr |
				myWonderland reportErrorToUser: 'Squeak could not determine what point of view to assign ' , myName , ' because ', msg.
				^ nil ].

		(aVector isKindOf: WonderlandActor)
			ifTrue: [
						positionVector _ aVector.
						rotationVector _ aVector.
					]
			ifFalse: [
						positionVector _ Array new: 3.
						positionVector at: 1 put: (aVector at: 1).
						positionVector at: 2 put: (aVector at: 2).
						positionVector at: 3 put: (aVector at: 3).

						rotationVector _ Array new: 3.
						rotationVector at: 1 put: (aVector at: 4).
						rotationVector at: 2 put: (aVector at: 5).
						rotationVector at: 3 put: (aVector at: 6).
					].

			myWonderland getUndoStack push: (UndoPOVChange for: self
														from: (self getPointOfView)).

			(aDuration = eachFrame) ifTrue: [
				^ self doEachFrame: [ 
										self moveToRightNow: positionVector undoable: false.
										self turnToRightNow: rotationVector undoable: false.
									]
											].

			self moveToRightNow: positionVector undoable: false.
			self turnToRightNow: rotationVector undoable: false.
			^ self ].

	^ self setPointOfView: aVector duration: aDuration style: gently.
