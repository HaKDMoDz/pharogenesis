readOnlyFileDoesNotExistUserHandling: fullFileName

	| dir files choices selection newName fileName |
	dir := FileDirectory forFileName: fullFileName.
	files := dir fileNames.
	fileName := FileDirectory localNameFor: fullFileName.
	choices := fileName correctAgainst: files.
	choices add: 'Choose another name'.
	choices add: 'Cancel'.
	selection := (PopUpMenu labelArray: choices lines: (Array with: 5) )
		startUpWithCaption: (FileDirectory localNameFor: fullFileName), '
does not exist.'.
	selection = choices size ifTrue:["cancel" ^ nil "should we raise another exception here?"].
	selection < (choices size - 1) ifTrue: [
		newName := (dir pathName , FileDirectory slash , (choices at: selection))].
	selection = (choices size - 1) ifTrue: [
		newName := FillInTheBlank 
							request: 'Enter a new file name' 
							initialAnswer: fileName].
	newName = '' ifFalse: [^ self readOnlyFileNamed: (self fullName: newName)].
	^ self error: 'Could not open a file'