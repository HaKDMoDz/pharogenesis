testDeleteDirectory
	"Test deletion of a directory"
	
	| aContainingDirectory preTestItems |
	aContainingDirectory := self myDirectory containingDirectory.
	preTestItems := aContainingDirectory fileAndDirectoryNames.
	
	self assert: self myAssuredDirectory exists.
	aContainingDirectory deleteDirectory: self myLocalDirectoryName.

	self shouldnt: 
		[aContainingDirectory directoryNames 
			includes: self myLocalDirectoryName ]
		description: 'Should successfully delete directory.'.
	self should: 
		[preTestItems = aContainingDirectory fileAndDirectoryNames]
		description: 'Should only delete the indicated directory.'.

	
	