purge
	"Replace my morph with a tombstone, if I am not in a world that is being shown."

	(self prePurge) ifNotNil: [
		contentsMorph become: (MorphObjectOut new xxxSetUrl: url page: self)].
		"Simple, isn't it!"