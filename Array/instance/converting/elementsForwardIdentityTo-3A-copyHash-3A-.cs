elementsForwardIdentityTo: otherArray copyHash: copyHash
	"This primitive performs a bulk mutation, causing all pointers to the elements of this array to be replaced by pointers to the corresponding elements of otherArray.  The identityHashes remain with the pointers rather than with the objects so that the objects in this array should still be properly indexed in any existing hashed structures after the mutation."
	<primitive: 249>
	self primitiveFailed