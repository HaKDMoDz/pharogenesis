loadForLocaleIsoString: localeString fromGzippedMimeLiteral: mimeString 
	"merge the translation from the mime literal."
	| stream localeID translator gs rbStream s currentPlatform |
	s := Base64MimeConverter mimeDecodeToBytes: mimeString readStream.
	s reset.
	gs := GZipReadStream on: s.
	rbStream := MultiByteBinaryOrTextStream with: gs contents asString.
	rbStream converter: UTF8TextConverter new.
	rbStream reset.
	localeID := LocaleID isoString: localeString.
	currentPlatform := Locale currentPlatform.
	
	[ Locale currentPlatform: (Locale localeID: localeID).
	stream := rbStream contents readStream ] ensure: [ Locale currentPlatform: currentPlatform ].
	translator := self localeID: localeID.
	translator loadFromStream: stream.
	LanguageEnvironment resetKnownEnvironments