generateWhileFalse: msgNode on: aStream indent: level
	"Generate C code for a loop in one of the following formats, as appropriate:
		while(!(cond)) { stmtList }
		do {stmtList} while(!(cond))
		while(1) {stmtListA; if (cond) break; stmtListB}"

	msgNode receiver statements size <= 1
		ifTrue: [^self generateWhileFalseLoop: msgNode on: aStream indent: level].
	msgNode args first isNilStmtListNode
		ifTrue: [^self generateDoWhileFalse: msgNode on: aStream indent: level].
	^self generateWhileForeverBreakTrueLoop: msgNode on: aStream indent: level