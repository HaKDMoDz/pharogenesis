createExternalFontFileForUnicodeKorean: fileName
"
	Smalltalk garbageCollect.
	StrikeFontSet createExternalFontFileForUnicodeKorean: 'uKoreanFont.out'.
"

	| file array f installDirectory |
	file := FileStream newFileNamed: fileName.
	installDirectory := Smalltalk at: #M17nInstallDirectory ifAbsent: [].
	installDirectory := installDirectory
		ifNil: [String new]
		ifNotNil: [installDirectory , FileDirectory pathNameDelimiter asString].
	array := Array
				with: (StrikeFont newForKoreanFromEFontBDFFile: installDirectory , 'b12.bdf' name: 'Korean10' overrideWith: 'shnmk12.bdf')
				with: ((StrikeFont newForKoreanFromEFontBDFFile: installDirectory , 'b14.bdf' name: 'Korean12' overrideWith: 'shnmk14.bdf') "fixAscent: 14 andDescent: 1 head: 1")
				with: ((StrikeFont newForKoreanFromEFontBDFFile: installDirectory , 'b16.bdf' name: 'Korean14' overrideWith: 'hanglg16.bdf') fixAscent: 16 andDescent: 4 head: 4)
				with: (StrikeFont newForKoreanFromEFontBDFFile: installDirectory , 'b24.bdf' name: 'Korean18' overrideWith: 'hanglm24.bdf').
	TextConstants at: #forceFontWriting put: true.
	f := ReferenceStream on: file.
	f nextPut: array.
	file close.
	TextConstants removeKey: #forceFontWriting.