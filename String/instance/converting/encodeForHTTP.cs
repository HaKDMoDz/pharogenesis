encodeForHTTP
	"change dangerous characters to their %XX form, for use in HTTP transactions"

	^ self encodeForHTTPWithTextEncoding: 'utf-8' conditionBlock: [:c | c isSafeForHTTP].
