changed
	"The default (super) method is, generally much slower than need be, since many transitions only change part of the screen on any given step of the animation.  The purpose of this method is to effect some of those savings."
	| loc box boxPrev h w |
	(stepNumber between: 1 and: nSteps) ifFalse: [^ super changed].
	effect = #slideBoth ifTrue: [^ super changed].
	effect = #slideOver ifTrue:
		[loc _ self stepFrom: self position - (self extent * direction) to: self position.
		^ self invalidRect: (((loc extent: self extent) expandBy: 1) intersect: bounds)].
	effect = #slideAway ifTrue:
		[loc _ self prevStepFrom: self position to: self position + (self extent * direction).
		^ self invalidRect: (((loc extent: self extent) expandBy: 1) intersect: bounds)].
	effect = #slideBorder ifTrue:
		[box _ endForm boundingBox translateBy:
				(self stepFrom: self topLeft - (self extent * direction) to: self topLeft).
		boxPrev _ endForm boundingBox translateBy:
				(self prevStepFrom: self topLeft - (self extent * direction) to: self topLeft).
		^ self invalidate: (box expandBy: 1) areasOutside: boxPrev].
	effect = #pageForward ifTrue:
		[loc _ self prevStepFrom: 0@0 to: self extent * direction.
		^ self invalidRect: (((bounds translateBy: loc) expandBy: 1) intersect: bounds)].
	effect = #pageBack ifTrue:
		[loc _ self stepFrom: self extent * direction negated to: 0@0.
		^ self invalidRect: (((bounds translateBy: loc) expandBy: 1) intersect: bounds)].
	effect = #frenchDoor ifTrue:
		[h _ self height. w _ self width.
		direction = #in ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: 0@h to: self extent).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: 0@h to: self extent).
			^ self invalidate: (box expandBy: 1) areasOutside: boxPrev].
		direction = #out ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: self extent to: 0@h).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: self extent to: 0@h).
			^ self invalidate: (boxPrev expandBy: 1) areasOutside: box].
		direction = #inH ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: w@0 to: self extent).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: w@0 to: self extent).
			^ self invalidate: (box expandBy: 1) areasOutside: boxPrev].
		direction = #outH ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: self extent to: w@0).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: self extent to: w@0).
			^ self invalidate: (boxPrev expandBy: 1) areasOutside: box]].
	effect = #zoomFrame ifTrue:
		[direction = #in ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: 0@0 to: self extent).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: 0@0 to: self extent).
			^ self invalidate: (box expandBy: 1) areasOutside: boxPrev].
		direction = #out ifTrue:
			[box _ Rectangle center: self center
							extent: (self stepFrom: self extent to: 0@0).
			boxPrev _ Rectangle center: self center
							extent: (self prevStepFrom: self extent to: 0@0).
			^ self invalidate: (boxPrev expandBy: 1) areasOutside: box]].
	effect = #zoom ifTrue:
		[box _ Rectangle center: self center extent:
			(direction = #in
				ifTrue: [self stepFrom: 0@0 to: self extent]
				ifFalse: [self prevStepFrom: self extent to: 0@0]).
		^ self invalidRect: ((box expandBy: 1) intersect: bounds)].
	^ super changed
