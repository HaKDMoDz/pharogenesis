deliverPainting: result evt: evt
	"Done painting.  May come from resume, or from original call.  Execute user's post painting instructions in the block.  Always use this standard one.  4/21/97 tk"

	| newBox newForm ans |
	palette ifNotNil: "nil happens" [palette setAction: #paint: evt: evt].	"Get out of odd modes"
	"rot _ palette getRotations."	"rotate with heading, or turn to and fro"
	"palette setRotation: #normal."
	result == #cancel ifTrue: [
		ans _ PopUpMenu withCaption: 'Do you really want to throw away 
what you just painted?' translated 
				chooseFrom: 'throw it away\keep painting it' translated.
		^ ans = 1 ifTrue: [self cancelOutOfPainting]
				ifFalse: [nil]].	"cancelled out of cancelling."

	"hostView rotationStyle: rot."		"rotate with heading, or turn to and fro"
	newBox _ paintingForm rectangleEnclosingPixelsNotOfColor: Color transparent.
	registrationPoint ifNotNil:
		[registrationPoint _ registrationPoint - newBox origin]. "relative to newForm origin"
	newForm _ 	Form extent: newBox extent depth: paintingForm depth.
	newForm copyBits: newBox from: paintingForm at: 0@0 
		clippingBox: newForm boundingBox rule: Form over fillColor: nil.
	newForm isAllWhite ifTrue: [
		(self valueOfProperty: #background) == true 
			ifFalse: [^ self cancelOutOfPainting]].

	newForm fixAlpha. "so alpha channel stays intact for 32bpp"

	self delete.	"so won't find me again"
	dimForm ifNotNil: [dimForm delete].
	newPicBlock value: newForm value: (newBox copy translateBy: bounds origin).
	ActiveWorld resumeScriptsPausedByPainting

