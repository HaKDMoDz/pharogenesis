primitiveVMParameter
	"Behaviour depends on argument count:
		0 args:	return an Array of VM parameter values;
		1 arg:	return the indicated VM parameter;
		2 args:	set the VM indicated parameter.
	VM parameters are numbered as follows:
		1	end of old-space (0-based, read-only)
		2	end of young-space (read-only)
		3	end of memory (read-only)
		4	allocationCount (read-only)
		5	allocations between GCs (read-write)
		6	survivor count tenuring threshold (read-write)
		7	full GCs since startup (read-only)
		8	total milliseconds in full GCs since startup (read-only)
		9	incremental GCs since startup (read-only)
		10	total milliseconds in incremental GCs since startup (read-only)
		11	tenures of surving objects since startup (read-only)
		12-20 specific to the translating VM
		21	root table size (read-only)
		22	root table overflows since startup (read-only)
		23	bytes of extra memory to reserve for VM buffers, plugins, etc.
		24	memory headroom when growing object memory (rw)
		25	memory threshold above which shrinking object memory (rw)
		26 interruptChecksEveryNms - force an ioProcessEvents every N milliseconds, in case the image is not calling getNextEvent often (rw)

	Note: Thanks to Ian Piumarta for this primitive."

	| mem paramsArraySize result arg index |
	mem _ self cCoerce: memory to: 'int'.
	self cCode: '' inSmalltalk: [mem _ 0].
	argumentCount = 0 ifTrue: [
		paramsArraySize _ 25.
		result _ self instantiateClass: (self splObj: ClassArray) indexableSize: paramsArraySize.
		0 to: paramsArraySize - 1 do:
			[:i | self storeWord: i ofObject: result withValue: ConstZero].
		self storeWord: 0	ofObject: result withValue: (self integerObjectOf: youngStart - mem).
		self storeWord: 1		ofObject: result withValue: (self integerObjectOf: freeBlock - mem).
		self storeWord: 2	ofObject: result withValue: (self integerObjectOf: endOfMemory - mem).
		self storeWord: 3	ofObject: result withValue: (self integerObjectOf: allocationCount).
		self storeWord: 4	ofObject: result withValue: (self integerObjectOf: allocationsBetweenGCs).
		self storeWord: 5	ofObject: result withValue: (self integerObjectOf: tenuringThreshold).
		self storeWord: 6	ofObject: result withValue: (self integerObjectOf: statFullGCs).
		self storeWord: 7	ofObject: result withValue: (self integerObjectOf: statFullGCMSecs).
		self storeWord: 8	ofObject: result withValue: (self integerObjectOf: statIncrGCs).
		self storeWord: 9	ofObject: result withValue: (self integerObjectOf: statIncrGCMSecs).
		self storeWord: 10	ofObject: result withValue: (self integerObjectOf: statTenures).
		self storeWord: 20	ofObject: result withValue: (self integerObjectOf: rootTableCount).
		self storeWord: 21	ofObject: result withValue: (self integerObjectOf: statRootTableOverflows).
		self storeWord: 22	ofObject: result withValue: (self integerObjectOf: extraVMMemory).
		self storeWord: 23	ofObject: result withValue: (self integerObjectOf: shrinkThreshold).
		self storeWord: 24	ofObject: result withValue: (self integerObjectOf: growHeadroom).
		self pop: 1 thenPush: result.
		^nil].

	arg _ self stackTop.
	(self isIntegerObject: arg) ifFalse: [^self primitiveFail].
	arg _ self integerValueOf: arg.
	argumentCount = 1 ifTrue: [	 "read VM parameter"
		(arg < 1 or: [arg > 25]) ifTrue: [^self primitiveFail].
		arg = 1		ifTrue: [result _ youngStart - mem].
		arg = 2		ifTrue: [result _ freeBlock - mem].
		arg = 3		ifTrue: [result _ endOfMemory - mem].
		arg = 4		ifTrue: [result _ allocationCount].
		arg = 5		ifTrue: [result _ allocationsBetweenGCs].
		arg = 6		ifTrue: [result _ tenuringThreshold].
		arg = 7		ifTrue: [result _ statFullGCs].
		arg = 8		ifTrue: [result _ statFullGCMSecs].
		arg = 9		ifTrue: [result _ statIncrGCs].
		arg = 10		ifTrue: [result _ statIncrGCMSecs].
		arg = 11		ifTrue: [result _ statTenures].
		((arg >= 12) and: [arg <= 20]) ifTrue: [result _ 0].
		arg = 21		ifTrue: [result _ rootTableCount].
		arg = 22		ifTrue: [result _ statRootTableOverflows].
		arg = 23		ifTrue: [result _ extraVMMemory].
		arg = 24		ifTrue: [result _ shrinkThreshold].
		arg = 25		ifTrue: [result _ growHeadroom].
		arg = 26		ifTrue: [result _ interruptChecksEveryNms]. 
		self pop: 2 thenPush: (self integerObjectOf: result).
		^nil].

	"write a VM parameter"
	argumentCount = 2 ifFalse: [^self primitiveFail].
	index _ self stackValue: 1.
	(self isIntegerObject: index) ifFalse: [^self primitiveFail].
	index _ self integerValueOf: index.
	index <= 0 ifTrue: [^self primitiveFail].
	successFlag _ false.
	index = 5 ifTrue: [
		result _ allocationsBetweenGCs.
		allocationsBetweenGCs _ arg.
		successFlag _ true].
	index = 6 ifTrue: [
		result _ tenuringThreshold.
		tenuringThreshold _ arg.
		successFlag _ true].
	index = 23 ifTrue: [
		result _ extraVMMemory.
		extraVMMemory _ arg.
		successFlag _ true].
	index = 24 ifTrue: [
		result _ shrinkThreshold.
		arg > 0 ifTrue:[
			shrinkThreshold _ arg.
			successFlag _ true]].
	index = 25 ifTrue: [
		result _ growHeadroom.
		arg > 0 ifTrue:[
			growHeadroom _ arg.
			successFlag _ true]].
	index = 26 ifTrue: [
		arg > 1 ifTrue:[
			result _ interruptChecksEveryNms.
			successFlag _ true]]. 

	successFlag ifTrue: [
		self pop: 3 thenPush: (self integerObjectOf: result).  "return old value"
		^ nil].

	self primitiveFail.  "attempting to write a read-only parameter"