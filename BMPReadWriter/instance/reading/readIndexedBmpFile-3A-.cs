readIndexedBmpFile: colors
	"Read uncompressed pixel data of depth d from the given BMP stream, where d is 1, 4, 8, or 16"
	| form bytesPerRow pixelData pixelLine startIndex map bitBlt mask |
	colors 
		ifNil:[form _ Form extent: biWidth@biHeight depth: biBitCount]
		ifNotNil:[form _ ColorForm extent: biWidth@biHeight depth: biBitCount.
				form colors: colors].
	bytesPerRow _ (((biBitCount* biWidth) + 31) // 32) * 4.
	pixelData _ ByteArray new: bytesPerRow * biHeight.
	biHeight to: 1 by: -1 do: [:y |
		pixelLine _ stream next: bytesPerRow.
		startIndex _ ((y - 1) * bytesPerRow) + 1.
		pixelData 
			replaceFrom: startIndex 
			to: startIndex + bytesPerRow - 1 
			with: pixelLine 
			startingAt: 1].
	form bits copyFromByteArray: pixelData.
	biBitCount = 16 ifTrue:[
		map := ColorMap shifts: #(8 -8 0 0) masks: #(16rFF 16rFF00 0 0).
		mask := 16r80008000.
	].
	biBitCount = 32 ifTrue:[
		map := ColorMap shifts: #(24 8 -8 -24) masks: #(16rFF 16rFF00 16rFF0000 16rFF000000).
		mask := 16rFF000000.
	].
	map ifNotNil:[
		bitBlt := BitBlt toForm: form.
		bitBlt sourceForm: form.
		bitBlt colorMap: map.
		bitBlt combinationRule: Form over.
		bitBlt copyBits.
	].
	mask ifNotNil:[
		bitBlt := BitBlt toForm: form.
		bitBlt combinationRule: 7 "bitOr:with:".
		bitBlt halftoneForm: (Bitmap with: mask).
		bitBlt copyBits.
	].
	^ form
