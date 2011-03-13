assertPragma: aString givesKeyword: aSymbol arguments: anArray
	| pragma decompiled |
	pragma := self pragma: aString selector: #zork.
	self assert: pragma keyword = aSymbol.
	self assert: pragma arguments = anArray.
	decompiled := (self class>>#zork) decompile.
	self assert: (decompiled properties pragmas includes: pragma). 
	self assert: (decompiled asString includesSubString: pragma asString).