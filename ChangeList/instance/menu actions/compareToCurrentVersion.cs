compareToCurrentVersion
	"If the current selection corresponds to a method in the system,
	then spawn a window showing the diffs as text"

	| change class s1 s2 |
	listIndex = 0 ifTrue: [^ self].
	change _ changeList at: listIndex.
	((class _ change methodClass) notNil
			and: [class includesSelector: change methodSelector])
	ifTrue:
		[s1 _ (class sourceCodeAt: change methodSelector) asString.
		s2 _ change string.
		s1 = s2 ifTrue: [^ self inform: 'Exact Match'].
		(StringHolder new textContents:
			(TextDiffBuilder buildDisplayPatchFrom: s1 to: s2))
			openLabel: 'Comparison to Current Version']
	ifFalse:
		[self flash].
