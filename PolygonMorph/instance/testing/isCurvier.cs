isCurvier
	"Test used by smoothing routines.  If true use true closed curve splines for closed curves. If not mimic old stodgy curveMorph curves with one sharp bend.. Override this routine in classes where backward compatability is still needed."
	^ (Preferences valueOfFlag: #Curvier)