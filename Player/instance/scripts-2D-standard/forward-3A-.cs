forward: dist 
	"Move forward (viz. in the direction of my heading) by the given amount"

	| rho radians delta didStray p aCostume aPlayfield |
	(aCostume _ self costume) isInWorld ifFalse: [^ self].
	aCostume isWorldOrHandMorph ifTrue: [^ self].
	aCostume owner isHandMorph ifTrue: [^ self].

	rho _ (aCostume asNumber: dist) asFloat.
	radians _ (self getHeadingUnrounded asFloat - 90.0) degreesToRadians.
	delta _ (radians cos @ radians sin) * rho.

	(aPlayfield _ aCostume pasteUpMorph) fenceEnabled ifTrue:
		[(aPlayfield bounds containsRect: aCostume bounds) ifFalse:
			["If I stray out of the bounds of my playfield, pull me back, but
			 without changing my heading as bounce would. Do nothing if
			 bounce has already corrected the direction."
			didStray _ false.
			((aCostume left < aPlayfield left and: [delta x < 0]) or:
			 [aCostume right > aPlayfield right and: [delta x > 0]]) ifTrue:
				[delta _ delta x negated @ delta y.
				didStray _ true].
			((aCostume top < aPlayfield top and: [delta y < 0]) or:
			 [aCostume bottom > aPlayfield bottom and: [delta y > 0]]) ifTrue:
				[delta _ delta x @ delta y negated.
				didStray _ true].
			(didStray and: [Preferences fenceSoundEnabled]) ifTrue: [aCostume makeFenceSound]]].

	"use and record the fractional position"
	p _ aCostume referencePosition + delta.
	aCostume referencePosition: p