windowMaximizeDownForm
	"Answer the form to use for window close buttons with mouse down and over."

	^self forms at: #windowMaximizeDown ifAbsent: [Form extent: 18@18 depth: Display depth]