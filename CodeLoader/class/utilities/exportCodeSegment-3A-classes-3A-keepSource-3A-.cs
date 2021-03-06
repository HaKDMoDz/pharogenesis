exportCodeSegment: exportName classes: aClassList keepSource: keepSources

	"Code for writing out a specific category of classes as an external image segment.  Perhaps this should be a method."

	| is oldMethods newMethods classList symbolHolder fileName |
	keepSources
		ifTrue: [
			self confirm: 'We are going to abandon sources.
Quit without saving after this has run.' orCancel: [^self]].

	classList := aClassList asArray.

	"Strong pointers to symbols"
	symbolHolder := Symbol allInstances.

	oldMethods := OrderedCollection new: classList size * 150.
	newMethods := OrderedCollection new: classList size * 150.
	keepSources
		ifTrue: [
			classList do: [:cl |
				cl selectors do:
					[:selector | | m oldCodeString methodNode |
					m := cl compiledMethodAt: selector.
					m fileIndex > 0 ifTrue:
						[oldCodeString := cl sourceCodeAt: selector.
						methodNode := cl compilerClass new
											parse: oldCodeString in: cl notifying: nil.
						oldMethods addLast: m.
						newMethods addLast: (m copyWithTempsFromMethodNode: methodNode)]]]].
	oldMethods asArray elementsExchangeIdentityWith: newMethods asArray.
	oldMethods := newMethods := nil.

	Smalltalk garbageCollect.
	is := ImageSegment new copyFromRootsForExport: classList.	"Classes and MetaClasses"

	fileName := FileDirectory fileName: exportName extension: ImageSegment fileExtension.
	is writeForExport: fileName.
	self compressFileNamed: fileName

