dependencyMenu: aMenu
	self fillMenu: aMenu fromSpecs: #(('change log...' showChangeLog)).
	self fillMenu: aMenu fromSpecs: #(('add dependency...' addDependency)).
	self selectedDependency ifNotNil: [
		self fillMenu: aMenu fromSpecs: #(('remove dependency...' remove))].
	^aMenu
