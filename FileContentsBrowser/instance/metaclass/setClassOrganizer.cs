setClassOrganizer
	"Install whatever organization is appropriate"
	| theClass |
	classOrganizer := nil.
	metaClassOrganizer := nil.
	classListIndex = 0 ifTrue: [^ self].
	classOrganizer := (theClass := self selectedClass) organization.
	metaClassOrganizer := theClass metaClass organization.
