createViews
	"Create a pluggable version of all the views for a Browser, including views and controllers."

	| hasSingleFile width topView packageListView classListView switchView messageCategoryListView messageListView browserCodeView infoView |
	contentsSymbol := self defaultDiffsSymbol.  "#showDiffs or #prettyDiffs"
	Smalltalk isMorphic ifTrue: [^ self openAsMorph].

	(hasSingleFile := self packages size = 1)
		ifTrue: [width := 150]
		ifFalse: [width := 200].

	(topView := StandardSystemView new) 
		model: self;
		borderWidth: 1.
		"label and minSize taken care of by caller"
	
	hasSingleFile 
		ifTrue: [
			self systemCategoryListIndex: 1.
			packageListView := PluggableListView on: self
				list: #systemCategorySingleton
				selected: #indexIsOne 
				changeSelected: #indexIsOne:
				menu: #packageListMenu:
				keystroke: #packageListKey:from:.
			packageListView window: (0 @ 0 extent: width @ 12)]
		ifFalse: [
			packageListView := PluggableListView on: self
				list: #systemCategoryList
				selected: #systemCategoryListIndex
				changeSelected: #systemCategoryListIndex:
				menu: #packageListMenu:
				keystroke: #packageListKey:from:.
			packageListView window: (0 @ 0 extent: 50 @ 70)].
	topView addSubView: packageListView.

	classListView := PluggableListView on: self
		list: #classList
		selected: #classListIndex
		changeSelected: #classListIndex:
		menu: #classListMenu:
		keystroke: #classListKey:from:.
	classListView window: (0 @ 0 extent: 50 @ 62).
	hasSingleFile 
		ifTrue: [topView addSubView: classListView below: packageListView]
		ifFalse: [topView addSubView: classListView toRightOf: packageListView].

	switchView := self buildInstanceClassSwitchView.
	switchView borderWidth: 1.
	topView addSubView: switchView below: classListView.

	messageCategoryListView := PluggableListView on: self
		list: #messageCategoryList
		selected: #messageCategoryListIndex
		changeSelected: #messageCategoryListIndex:
		menu: #messageCategoryMenu:.
	messageCategoryListView window: (0 @ 0 extent: 50 @ 70).
	topView addSubView: messageCategoryListView toRightOf: classListView.

	messageListView := PluggableListView on: self
		list: #messageList
		selected: #messageListIndex
		changeSelected: #messageListIndex:
		menu: #messageListMenu:
		keystroke: #messageListKey:from:.
	messageListView window: (0 @ 0 extent: 50 @ 70).
	topView addSubView: messageListView toRightOf: messageCategoryListView.

	browserCodeView := MvcTextEditor default on: self 
			text: #contents accept: #contents:notifying:
			readSelection: #contentsSelection menu: #codePaneMenu:shifted:.
	browserCodeView window: (0@0 extent: width@110).
	topView 
		addSubView: browserCodeView 
		below: (hasSingleFile 
			ifTrue: [switchView]
			ifFalse: [packageListView]).

	infoView := StringHolderView new
		model: self infoString;
		window: (0@0 extent: width@12);
		borderWidth: 1.
	topView addSubView: infoView below: browserCodeView.

	^ topView
