openBrowserView: aBrowserView label: aString 
	"Schedule aBrowserView, labelling the view aString."

	aBrowserView isMorph
		ifTrue:  [(aBrowserView setLabel: aString) openInWorld]
		ifFalse: [aBrowserView label: aString.
				aBrowserView minimumSize: 300 @ 200.
				aBrowserView subViews do: [:each | each controller].
				aBrowserView controller open].

	^ aBrowserView model
