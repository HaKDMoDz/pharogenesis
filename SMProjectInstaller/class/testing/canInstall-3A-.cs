canInstall: aPackage
	"Answer if this class can install the package.
	We handle .pr files (upper and lowercase)"

	| fileName |
	fileName _ aPackage downloadFileName.
	fileName ifNil: [^false].
	^'pr' = (FileDirectory extensionFor: fileName) asLowercase