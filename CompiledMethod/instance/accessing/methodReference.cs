methodReference
	| class selector |
	class := self methodClass ifNil: [^nil].
	selector := self selector ifNil: [^nil].
	^MethodReference class: class selector: selector.
	