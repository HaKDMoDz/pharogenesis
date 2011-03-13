updateFrom7015
	"self new updateFrom7015"
	"
	Change Set:		ProportionalSplitter-honor-boundaries-jrp
	Date:			21 March 2006
	Author:			John Pierce

	Keep splitter from moving outside the natural window boundaries
	-----------
	Change Set:		projectViewMenuFix-yo.cs
	Date:			2 August 2005
	Author:			Kazuhiro Abe and Yoshiki Ohshima

	The project name of menu label was incorrect.
	------------
	Change Set:		testTileFix-kfr
	Date:			24 January 2006
	Author:			Karl Ramberg

	When clicking the button to add a new TestTile the ScriptEditor is unresponsive to place the TestTile 	
	because the tile never signaled a mouseEnter to the ScriptEditor. This is a fix for that.
	-------------
	Change Set:		retainRotationCenter-sw
	Date:			25 January 2006
	Author:			Scott Wallace

	When repainting a sketch, retain any existing rotation-center that may have been specified.
	--------------
	Change Set:		undoFixes-sw
	Date:			2 February 2006
	Author:			Scott Wallace

	Implements uindo for 'dismiss via halo', which works even if trash is not being preserved.
	Make the Undo button in the NavBar dynamically show what action it *would* undo.
	Makes that button work propery in a redo situation (formerly it had malfunctioned  when in redo 
	mode.)
	Elaborates the wording of the 'undo' item to include identification of the target, for resize and rotate 	now, as it always has done for move.
	Fixes some bugs encountered along the way.
	Fixes the bug that could break undo of a rotation, if an addition or removal of a flex shell was 	involved.
	------------------
	Change Set:		SuperSwikiString-KR
	Date:			1 February 2006
	Author:			Korakurider

	Current m18n implementation assumes shift_jis encoding is used in wire protocol between
	SuperSwiki server and client.  Then some existing projects encoded in other than shift_jis
	cause walkback. 

	This patch introduces 'encodingName' for ServerDirectory entry (even in knownServer entry) 
	so that appropriate encoding can be specified for each server.  If not specified, encoding is 	
	determined based on current locale.
	------------------
	Change Set:		rotCenterHaloHelp-sw
	Date:			16 February 2006
	Author:			Scott Wallace

	Supplements the balloon help for the rotation-center handle with the additional informaiton that the 
	user should hold down the shift key to move it.
	-------------------
	Name: Balloon-mir.11
	Author: mir
	Time: 22 March 2006, 2:14:18 pm

	(backported from Jan Fietz' changes)

	FlashFileReader support for more image tags:
		processDefineBitsJPEG3 (JPEG + alpha channel)
		processDefineLossless2
	---------------------
	Name: Flash-mir.2
	Author: mir
	Time: 22 March 2006, 3:04 pm

	merge with iSqueak flash enh. (was ballon)
	-----------------------
	Name: SUnitGUI-lr.6
	Author: lr
	Time: 22 March 2006, 7:49:27 pm

	- fixed a couple of issues related to the change-notifications
		- no more debugger if the system contains classes that are not categorized yet (can happen 				when loading mcz files)
		- register for some different change events that were missed
	- unregister for changes when window closes
	-------------------
	"
	
	self script53.
	self flushCaches.
	MCDefinition clearInstances.