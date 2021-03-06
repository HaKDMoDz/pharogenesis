objectForDataStream: refStrm
	"I am about to be written on an object file.  Write a reference to a class in Smalltalk instead."

	refStrm insideASegment
		ifFalse: ["Normal use"
			^ DiskProxy global: self theNonMetaClass name selector: #withClassVersion:
				args: {self classVersion}]
		ifTrue: ["recording objects to go into an ImageSegment"
			self isSystemDefined ifFalse: [^ self].		"do trace Player classes"
			(refStrm rootObject includes: self) ifTrue: [^ self].
				"is in roots, intensionally write out, ^ self"
			
			"A normal class.  remove it from references.  Do not trace."
			refStrm references removeKey: self ifAbsent: []. 	"already there"
			^ nil]
