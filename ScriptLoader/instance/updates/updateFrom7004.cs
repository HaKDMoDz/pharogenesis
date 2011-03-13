updateFrom7004
	"self new updateFrom7004"
	"
	0001613: [ENH] truncatedChanges-ls
	Date: 4 September 2003
	Author: Lex Spoon

	Handle truncated changes files more gracefully. The problem actually
	can happen, and has been observed at Georgia Tech, e.g. if someone
	downloads a file but does not complete the download. The current
	behavior is that pink debuggers pop up; this is extremely confusing for
	new users when it happens in places like the Browser.

	The solution in this changeset is to fall back on the decompiler. A
	more thorough solution should probably trigger a systematic invalidation
	of all source pointers past the end of the changes file, because as time
	goes on the changes file will eventually grow large enough to cover the
	lost code and the routine will start returning random method code for
	the invalid source pointers.
	----------------
	0001354: MIDI port name is garbled in selection dialog
	Change Set:		AddTransMIDIPort
	Date:			31 May 2005
	Author:			Tooru Nosse
	
	fix for query of MIDI portName with SJIS name
	enable translation for midi port selection
	----------------
	0002341: MorphicEventDispatcher minor refactoring
	0001284: Smalltalk condenseSources leads to UndefinedObject(Object)>>error:
	0003180: Selector requester does not strip blanks
	tests from 0001798: #critical:ifError: on Semaphore does not always signal the semaphore
	test from 0003133: [BUG][FIX] Float>>#asIEEE32BitWord and #fromIEEE32Bit:
	----------------
	Change Set: dupAllCtrlAltKeysPref-dew
	Date: 13 December 2004
	Author: Doug Way

	Adds a new preference duplicateAllCtrlAndAltkeys to duplicate all Ctrl and Alt modifier key 	
	commands, which causes all Ctrl key commands to behave the same as Alt-key commands. 	
	The original Ctrl (a.k.a. 'shift') key commands are still available via Ctrl-Shift and Alt-Shift.

	This differs from the existing ctrlAlt prefs which only duplicate/swap the eight editing keys. 	
	Also includes code to prevent these other two conflicting prefs from being activated at the 
	same time.

	The three related prefs (swapCtrlAndAltKeys, duplicateCtrlAndAltKeys, 	
	duplicateAllCtrlAndAltKeys) should really be made into a radio button preferences set 
	sometime in the future (like the Halo look preferences), since only one can be turned on at 
	a time. This would eliminate the need for the #changed methods.

	See 'modifier' discussion on squeak-dev from 12/3/2004 for more details on why this was 	provided.
	-----------
	- postCopy for MethodProperties
	- Preferences panel size
	- remove Preference #ansiAssignmentOperatorWhenPrettyPrinting
	- remove Preference #resizeOnAllSides, #twoSidedPoohTextures
	- set selector when evaluating a #Doit. Fixes MessageTally tallySends:
	- cleanups in Parser: remove old Properties code
	- cleanups in Encoder: use #binding, not #associationFor:. 
	- remove the last new initialize cases
	- Fix ClassTestCase to not turn off Deprecation
	- Fix Tests for ObjectsAsMethods
	- Refactor/deprecate old environment methods in SystemDictionary
	Graphics:
	-----------
	- 0002449: [Enh] Adds a target-sighting cursor to the menagerie of cursors.
	- 0002450: [Fix] Form fromUser can create a form w/ zero area if user specifies
	   a corner to upper left of origin.
	- bullet-proof ImageReadWriter class>>understandsImageFormat:
	- 0002684: [Fix] In 6719 rotateBy: #right or #left returns wrong aspect ratio.
	- 0003060: Graphics-md.27: additions from Connectors / merge with image
	--------
	Traits:
	- Fix PackageInfo: extension methods defined in traits were missing in package
	- Move traits related changes of methods from Monticello into correct package PackageInfo 
	"
	self script43.
	self flushCaches.
	Preferences removePreference: #ansiAssignmentOperatorWhenPrettyPrinting.
	Preferences removePreference: #resizeOnAllSides.
	Preferences removePreference: #twoSidedPoohTextures.
	Smalltalk removeEmptyMessageCategories.	
	SystemOrganization removeEmptyCategories.
