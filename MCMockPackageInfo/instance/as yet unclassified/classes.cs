classes
	^ self classNames 
		select: [:name | Smalltalk hasClassNamed: name]
		thenCollect: [:name | Smalltalk at: name]