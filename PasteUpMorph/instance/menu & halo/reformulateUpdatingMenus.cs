reformulateUpdatingMenus
	"Give any updating menu morphs in the receiver a fresh kiss of life"

	(self submorphs select: [:m | m isKindOf: UpdatingMenuMorph]) do:
		[:m | m updateMenu] 

	"NB: to do the perfect job here one might well want to extend across allMorphs here, but the expense upon project entry is seemingly too high a price to pay at this point"