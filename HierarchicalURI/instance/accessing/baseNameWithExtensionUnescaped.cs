baseNameWithExtensionUnescaped
	"returns the last component foo.bar as unescaped "

	^self pathComponents last unescapePercents.
