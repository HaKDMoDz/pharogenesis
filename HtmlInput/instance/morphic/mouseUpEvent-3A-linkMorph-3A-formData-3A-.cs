mouseUpEvent: event linkMorph: linkMorph formData: formData
	| aPoint |
	aPoint _ event cursorPoint - linkMorph topLeft.
	formData addInput: (HiddenInput name: (value, '.x') value: aPoint x asInteger asString).
	formData addInput: (HiddenInput name: (value, '.y') value: aPoint y asInteger asString).
	formData submit