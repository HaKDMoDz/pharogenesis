maxDraggableStackSize: dropIntoEmptyStack
	"Note: dropIntoEmptyStack, means one less empty stack to work with.
		This needs to be reevaluated at time of drop."
	"Not super smart - doesn't use stacks that are buildable though not empty"

	| nFree nEmptyStacks |
	nFree _ (freeCells select: [:d | d hasCards not]) size.
	nEmptyStacks _ (stacks select: [:d | d hasCards not]) size.
	dropIntoEmptyStack ifTrue: [nEmptyStacks _ nEmptyStacks - 1].
	^ (1 + nFree) * (2 raisedTo: nEmptyStacks)