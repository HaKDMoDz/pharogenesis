httpGif: url
	"Fetch the given URL, parse it using the GIF reader, and return the resulting Form."
	"	HTTPSocket httpShowGif: 'www.altavista.digital.com/av/pix/default/av-adv.gif'	 "
	"	HTTPSocket httpShowGif: 'www.webPage.com/~kaehler2/ainslie.gif'	 "

	| doc ggg |
	doc _ self httpGet: url accept: 'image/gif'.
	doc class == String ifTrue: [
		self inform: 'The server with that GIF is not responding'.
		^ ColorForm extent: 20@20 depth: 8].
	doc binary; reset.
	(ggg _ GIFReadWriter new) setStream: doc.
	^ ggg nextImage.
