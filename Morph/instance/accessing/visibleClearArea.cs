visibleClearArea
	"Answer the receiver visible clear area. The intersection 
	between the clear area and the viewbox."
	^ self viewBox intersect: self clearArea