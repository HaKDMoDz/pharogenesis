morphToInstall: aMorph font: aFont
	"Create a new tab consisting of a string holding the morph's name"
	| aLabel nameToUse |
	aLabel _ StringMorph contents: (nameToUse _ aMorph externalName) font: aFont.
	self addMorph: aLabel.
	aLabel lock.
	self referent: aMorph.
	self setNameTo: nameToUse.
	self fitContents.