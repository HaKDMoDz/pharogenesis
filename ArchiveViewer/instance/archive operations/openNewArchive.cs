openNewArchive
	| menu result |
	menu _ StandardFileMenu oldFileMenu: (FileDirectory default) withPattern: '*.zip'.
	result := menu startUpWithCaption: 'Select Zip archive to open...'.
	result ifNil: [ ^self ].
	self fileName: (result directory fullNameFor: result name).
