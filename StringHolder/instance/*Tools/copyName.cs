copyName
	"Copy the current selector to the clipboard"
	| selector |
	(selector := self selectedMessageName) ifNotNil:
		[Clipboard clipboardText: selector asString asText]