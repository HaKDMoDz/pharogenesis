version
	^ version ifNil:
		[Cursor wait showWhile:
			[version := repository versionFromFileNamed: selectedVersion].
		version]