name
	"Return this color's name, or nil if it has no name. Only returns a name if it exactly matches the named color."

	ColorNames do:
		[:name | (Color perform: name) = self ifTrue: [^ name]].
	^ nil
