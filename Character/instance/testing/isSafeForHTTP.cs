isSafeForHTTP
	"whether a character is 'safe', or needs to be escaped when used, eg, in a URL"
	"[GG]  See http://www.faqs.org/rfcs/rfc1738.html. ~ is unsafe and has been removed"
	^ value < 128
		and: [self isAlphaNumeric
				or: ['.-_' includes: self]]