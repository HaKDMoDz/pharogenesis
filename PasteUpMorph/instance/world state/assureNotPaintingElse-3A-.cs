assureNotPaintingElse: aBlock
	"If painting is already underway in the receiver, put up an informer to that effect and evalute aBlock"

	self sketchEditorOrNil ifNotNil:
		[self inform: 'Sorry, you can only paint
one object at a time' translated.
		Cursor normal show.
		^ aBlock value]
