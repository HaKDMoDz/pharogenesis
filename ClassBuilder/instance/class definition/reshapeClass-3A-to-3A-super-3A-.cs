reshapeClass: aClass to: templateClass super: newSuper
	"Reshape the given class to the new super class.
	If templateClass is not nil then it defines the shape of the new class"
	| fmt newClass newMeta newSuperMeta oldMeta instVars oldClass |
	templateClass == nil
		ifTrue:[oldClass _ aClass]
		ifFalse:[oldClass _ templateClass].
	aClass becomeUncompact.
	"Compute the new format of the class"
	instVars _ instVarMap at: aClass name ifAbsent:[oldClass instVarNames].
	fmt _ self computeFormat: oldClass typeOfClass
				instSize: instVars size
				forSuper: newSuper
				ccIndex: 0."Known to be 0 since we uncompacted aClass first"
	fmt == nil ifTrue:[^nil].
	aClass isMeta ifFalse:["Create a new meta class"
		oldMeta _ aClass class.
		newMeta _ oldMeta clone.
		newSuperMeta _ newSuper ifNil:[Class] ifNotNil:[newSuper class].
		newMeta 
			superclass: newSuperMeta
			methodDictionary: MethodDictionary new
			format: (self computeFormat: oldMeta typeOfClass 
							instSize: oldMeta instVarNames size 
							forSuper: newSuperMeta
							ccIndex: 0);
			setInstVarNames: oldMeta instVarNames;
			organization: oldMeta organization.
		"Recompile the meta class"
		oldMeta hasMethods 
			ifTrue:[newMeta compileAllFrom: oldMeta].
		"Fix up meta class structure"
		oldMeta superclass removeSubclass: oldMeta.
		newMeta superclass addSubclass: newMeta.
		"And record the change so we can fix global refs later"
		self recordClass: oldMeta replacedBy: newMeta.
	].
	newClass _ newMeta == nil
		ifTrue:[oldClass clone]
		ifFalse:[newMeta adoptInstance: oldClass from: oldMeta].
	newClass
		superclass: newSuper
		methodDictionary: MethodDictionary new
		format: fmt;
		setInstVarNames: instVars;
		organization: aClass organization.

	"Recompile the new class"
	aClass hasMethods 
		ifTrue:[newClass compileAllFrom: aClass].

	"Export the new class into the environment"
	aClass isMeta ifFalse:[
		"Derefence super sends in the old class"
		self fixSuperSendsFrom: aClass.
		"Export the class"
		environ at: newClass name put: newClass.
		"And use the ST association in the new class"
		self fixSuperSendsTo: newClass].

	"Fix up the class hierarchy"
	aClass superclass removeSubclass: aClass.
	newClass superclass addSubclass: newClass.

	"Adopt all the instances of the old class"
	aClass autoMutateInstances
		ifTrue:[newClass updateInstancesFrom: aClass].

	"And record the change"
	self recordClass: aClass replacedBy: newClass.

	^newClass