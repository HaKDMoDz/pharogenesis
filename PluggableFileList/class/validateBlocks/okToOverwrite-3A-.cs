okToOverwrite: aString

	"Answer whether user confirms that it is ok to overwrite the file named in aString"
	^ 1 = ((PopUpMenu
		labels:
'overwrite that file
select another file')
		startUpWithCaption: aString, '
already exists.')
