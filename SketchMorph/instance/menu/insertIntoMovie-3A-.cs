insertIntoMovie: evt

	| movies aTarget |
	movies :=
		(self world rootMorphsAt: evt hand targetPoint)
			select: [:m | ((m isKindOf: MovieMorph) or:
						 [m isSketchMorph]) and: [m ~= self]].
	movies isEmpty ifTrue: [^ self].
	aTarget := movies first.
	(aTarget isSketchMorph) ifTrue: [
		aTarget := aTarget replaceSelfWithMovie].
	aTarget insertFrames: (Array with: self).
	self delete.
