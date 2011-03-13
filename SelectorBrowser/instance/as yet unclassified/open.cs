open
	"Create a Browser that lets you type part of a selector, shows a list of selectors,
	shows the classes of the one you chose, and spwns a full browser on it.
		SelectorBrowser new open
	"

	| selectorListView typeInView topView classListView exampleView |
	Smalltalk isMorphic ifTrue: [^ self openAsMorph].

	selectorIndex := classListIndex := 0.
	topView := (StandardSystemView new) model: self.
	topView borderWidth: 1.
		"label and minSize taken care of by caller"

	typeInView := PluggableTextView on: self 
			text: #contents accept: #contents:notifying:
			readSelection: #contentsSelection menu: #codePaneMenu:shifted:.
	typeInView window: (0@0 extent: 50@14);
		askBeforeDiscardingEdits: false.
	topView addSubView: typeInView.

	selectorListView := PluggableListView on: self
		list: #messageList
		selected: #messageListIndex
		changeSelected: #messageListIndex:
		menu: #selectorMenu:
		keystroke: #messageListKey:from:.
	selectorListView menuTitleSelector: #selectorMenuTitle.
	selectorListView window: (0 @ 0 extent: 50 @ 46).
	topView addSubView: selectorListView below: typeInView.

	classListView := PluggableListView on: self
		list: #classList
		selected: #classListIndex
		changeSelected: #classListIndex:
		menu: nil	"never anything selected"
		keystroke: #arrowKey:from:.
	classListView menuTitleSelector: #classListSelectorTitle.
	classListView window: (0 @ 0 extent: 50 @ 60).
	topView addSubView: classListView toRightOf: typeInView.

	exampleView := PluggableTextView on: self 
			text: #byExample accept: #byExample:
			readSelection: #contentsSelection menu: #codePaneMenu:shifted:.
	exampleView window: (0@0 extent: 100@40);
		askBeforeDiscardingEdits: false.
	topView addSubView: exampleView below: selectorListView.


	topView label: 'Method Finder'.
	"topView minimumSize: 350@250; maximumSize: 350@250."
	topView subViews do: [:each | each controller].
	topView controller open.

