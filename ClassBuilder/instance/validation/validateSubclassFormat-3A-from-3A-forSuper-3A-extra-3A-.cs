validateSubclassFormat: newType from: oldClass forSuper: newSuper extra: newInstSize
	"Validate the # of instVars and the format of the subclasses"
	| deltaSize oldType |
	oldClass == nil ifTrue: [^ true]. "No subclasses"
	"Compute the # of instvars needed for all subclasses"
	deltaSize _ newInstSize.
	(oldClass notNil)
		ifTrue: [deltaSize _ deltaSize - oldClass instVarNames size].
	(newSuper notNil)
		ifTrue: [deltaSize _ deltaSize + newSuper instSize].
	(oldClass notNil and: [oldClass superclass notNil]) 
		ifTrue: [deltaSize _ deltaSize - oldClass superclass instSize].
	oldClass == nil ifTrue: [
		deltaSize > 254 ifTrue: [
			self error: 'More than 254 instance variables'.
			^ false].
		^ true].
	oldClass withAllSubclassesDo: [:sub |
		sub instSize + deltaSize > 254 ifTrue: [
			self error: sub name,' has more than 254 instance variables'.
			^ false]].
	newType ~~ #normal ifTrue:
		["And check if the immediate subclasses of oldClass can keep its layout"
		oldClass subclassesDo:[:sub|
			oldType _ sub typeOfClass.
			oldType == newType ifFalse: [
				self error: sub name,' cannot be recompiled'.
				^ false]]].
	^ true