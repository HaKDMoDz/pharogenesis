getVMParameters	"Smalltalk getVMParameters"
	"Answer an Array containing the current values of the VM's internal
	parameter/metric registers.  Each value is stored in the array at the
	index corresponding to its VM register.  (See #vmParameterAt: and
	#vmParameterAt:put:.)"

	^self deprecated: 'Use SmalltalkImage current getVMParameters'
		block: [SmalltalkImage current getVMParameters] .
