explorerContents
	^{
		'hexadecimal' -> 16.
		'octal' -> 8.
		'binary' -> 2
	} collect: [:each |
		ObjectExplorerWrapper with: each key translated name: (self printStringBase: each value) model: self]