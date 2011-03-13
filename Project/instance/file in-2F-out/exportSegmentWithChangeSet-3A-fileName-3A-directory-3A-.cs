exportSegmentWithChangeSet: aChangeSetOrNil fileName: aFileName
directory: aDirectory
	"Store my project out on the disk as an *exported*
ImageSegment.  All outPointers will be in a form that can be resolved
in the target image.  Name it <project name>.extSeg.  Whatdo we do
about subProjects, especially if they are out as local image
segments?  Force them to come in?
	Player classes are included automatically."

	| is str ans revertSeg roots holder collector fd mgr stacks |

	"Files out a changeSet first, so that a project can contain
its own classes"
world isMorph ifFalse: [
	self projectParameters at: #isMVC put: true.
	^ false].	"Only Morphic projects for now"
world ifNil: [^ false].  world presenter ifNil: [^ false].

Utilities emptyScrapsBook.
world currentHand pasteBuffer: nil.	  "don't write the paste buffer."
world currentHand mouseOverHandler initialize.	  "forget about any
references here"
	"Display checkCurrentHandForObjectToPaste."
Command initialize.
world clearCommandHistory.
world fullReleaseCachedState; releaseViewers.
world cleanseStepList.
world localFlapTabs size = world flapTabs size ifFalse: [
	self error: 'Still holding onto Global flaps'].
world releaseSqueakPages.
ScriptEditorMorph writingUniversalTiles: (self projectParameterAt:
#universalTiles ifAbsent: [false]).
holder _ Project allProjects.	"force them in to outPointers, where
DiskProxys are made"

"Just export me, not my previous version"
revertSeg _ self projectParameters at: #revertToMe ifAbsent: [nil].
self projectParameters removeKey: #revertToMe ifAbsent: [].

roots _ OrderedCollection new.
roots add: self; add: world; add: transcript; add: changeSet; add: thumbnail.
roots add: world activeHand.

	"; addAll: classList; addAll: (classList collect: [:cls | cls class])"

roots _ roots reject: [ :x | x isNil].	"early saves may not have
active hand or thumbnail"

	fd _ aDirectory directoryNamed: self resourceDirectoryName.
	fd assureExistence.
	"Clean up resource references before writing out"
	mgr _ self resourceManager.
	self resourceManager: nil.
	ResourceCollector current: ResourceCollector new.
	ResourceCollector current localDirectory: fd.
	ResourceCollector current baseUrl: self resourceUrl.
	ResourceCollector current initializeFrom: mgr.
	ProgressNotification signal: '2:findingResources' extra:
'(collecting resources...)' translated.
	"Must activate old world because this is run at #armsLength.
	Otherwise references to ActiveWorld, ActiveHand, or ActiveEvent
	will not be captured correctly if referenced from blocks or user code."
	world becomeActiveDuring:[
		is _ ImageSegment new copySmartRootsExport: roots asArray.
		"old way was (is _ ImageSegment new
copyFromRootsForExport: roots asArray)"
	].
	self resourceManager: mgr.
	collector _ ResourceCollector current.
	ResourceCollector current: nil.
	ProgressNotification signal: '2:foundResources' extra: ''.
	is state = #tooBig ifTrue: [
		collector replaceAll.
		^ false].

str _ ''.
"considered legal to save a project that has never been entered"
(is outPointers includes: world) ifTrue: [
	str _ str, '\Project''s own world is not in the segment.' translated withCRs].
str isEmpty ifFalse: [
	ans _ (PopUpMenu labels: 'Do not write file
Write file anyway
Debug' translated) startUpWithCaption: str.
	ans = 1 ifTrue: [
		revertSeg ifNotNil: [projectParameters at:
#revertToMe put: revertSeg].
		collector replaceAll.
		^ false].
	ans = 3 ifTrue: [
		collector replaceAll.
		self halt: 'Segment not written' translated]].
	stacks _ is findStacks.

	is
		writeForExportWithSources: aFileName
		inDirectory: fd
		changeSet: aChangeSetOrNil.
	SecurityManager default signFile: aFileName directory: fd.
	"Compress all files and update check sums"
	collector forgetObsolete.
	self storeResourceList: collector in: fd.
	self storeHtmlPageIn: fd.
	self storeManifestFileIn: fd.
	self writeStackText: stacks in: fd registerIn: collector.
	"local proj.005.myStack.t"
	self compressFilesIn: fd to: aFileName in: aDirectory
resources: collector.
			"also deletes the resource directory"
	"Now update everything that we know about"
	mgr updateResourcesFrom: collector.

revertSeg ifNotNil: [projectParameters at: #revertToMe put: revertSeg].
holder.

collector replaceAll.

world flapTabs do: [:ft |
		(ft respondsTo: #unhibernate) ifTrue: [ft unhibernate]].
is arrayOfRoots do: [:obj |
	obj class == ScriptEditorMorph ifTrue: [obj unhibernate]].
^ true
