smallObjectCatalogIcon
	"Private - Generated method"
	^ Icons
			at: #'smallObjectCatalog'
			ifAbsentPut:[ Form fromBinaryStream: (Base64MimeConverter mimeDecodeToBytes: self smallObjectCatalogIconContents readStream) ].