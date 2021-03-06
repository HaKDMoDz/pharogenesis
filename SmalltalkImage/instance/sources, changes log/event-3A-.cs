event: anEvent
	"Hook for SystemChangeNotifier"

	(anEvent isRemoved and: [anEvent itemKind = SystemChangeNotifier classKind]) ifTrue: [
		anEvent item acceptsLoggingOfCompilation 
			ifTrue: [self logChange: 'Smalltalk removeClassNamed: #' , anEvent item name].
	].
	anEvent isDoIt 
		ifTrue: [self logChange: anEvent item].
	(anEvent isRemoved and: [anEvent itemKind = SystemChangeNotifier methodKind]) ifTrue: [
		anEvent itemClass acceptsLoggingOfCompilation 
			ifTrue: [self logChange: anEvent itemClass name , ' removeSelector: #' , anEvent itemSelector]].