bmpColorsFrom: aBinaryStream count: colorCount depth: depth
	"Read colorCount BMP color map entries from the given binary stream. Answer an array of Colors."

	| maxLevel colors b g r |
	colorCount = 0 ifTrue: [  "this BMP file does not have a color map"
		"default monochrome color map"
		depth = 1 ifTrue: [^ Array with: Color white with: Color black].
		"default gray-scale color map"
		maxLevel _ (2 raisedTo: depth) - 1.
		^ (0 to: maxLevel) collect: [:level | Color gray: (level asFloat / maxLevel)]].

	colors _ Array new: colorCount.
	1 to: colorCount do: [:i |
		b _ aBinaryStream next.
		g _ aBinaryStream next.
		r _ aBinaryStream next.
		aBinaryStream skip: 1.
		colors at: i put: (Color r: r g: g b: b range: 255)].
	^ colors
