refresh
	"hack to refresh item given an object and a string that is either an index or an instance variable name."
	[ | index |
		(model class allInstVarNames includes: itemName)
			ifTrue: [ item := model instVarNamed: itemName ]
			ifFalse: [ index := itemName asNumber.
				(index between: 1 and: model basicSize) ifTrue: [ item := model basicAt: index]]
	] on: Error do: [ :ex | item := nil ]