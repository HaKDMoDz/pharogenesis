fixUponLoad: aProject seg: anImageSegment
	"We are in an old project that is being loaded from disk.
Fix up conventions that have changed."

	self isWorldMorph ifTrue: [
			(self valueOfProperty: #soundAdditions) ifNotNilDo:
				[:additions | SampledSound
assimilateSoundsFrom: additions]].

	^ super fixUponLoad: aProject seg: anImageSegment