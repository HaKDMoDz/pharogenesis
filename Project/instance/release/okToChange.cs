okToChange
	| ok hasSubProjects itsName is list |
	hasSubProjects _ world isMorph
		ifTrue: [(world submorphs select:
						[:m | (m isKindOf: SystemWindow)
								and: [m model isKindOf: Project]]) size > 0]
		ifFalse: [(world controllerWhoseModelSatisfies:
						[:m | m isKindOf: Project]) notNil].
	hasSubProjects ifTrue:
		[PopUpMenu notify: 
'The project ', self name printString, '
contains sub-projects.  You must remove these
explicitly before removing their parent.'.
		^ false].
	ok _ world isMorph not and: [world scheduledControllers size <= 1].
	ok ifFalse: [self isMorphic ifTrue: [
		self parent == Project current 
			ifFalse: [^true]]].  "view from elsewhere.  just delete it."
	ok _ (self confirm:
'Really delete the project
', self name printString, '
and all its windows?').
		
	ok ifFalse: [^ false].

	"about to delete this project; clear previous links to it from other Projects:"
	ImageSegment allSubInstancesDo: [:seg |
		seg ifOutPointer: self thenAllObjectsDo: [:obj |
			(obj isKindOf: ProjectViewMorph) ifTrue: [
				obj deletingProject: self.  obj abandon].
			obj class == Project ifTrue: [obj deletingProject: self]]].
	Project allProjects do: [:p | p deletingProject: self].	"ones that are in"
	ProjectViewMorph allSubInstancesDo: [:p | 
		p deletingProject: self.
		p project == self ifTrue: [p abandon]].

	world isMorph 
		ifTrue: [world submorphs do:   "special release for wonderlands"
						[:m | (m isKindOf: WonderlandCameraMorph)
								and: [m getWonderland release]].
			"Remove Player classes and metaclasses owned by project"
			is _ ImageSegment new arrayOfRoots: (Array with: self).
			(list _ is rootsIncludingPlayers) ifNotNil: [
				(list copyWithout: self) do: [:playerCls | 
					playerCls isMeta ifFalse: [
						playerCls removeFromSystemUnlogged]]]].

	(changeSet isEmpty and: [(changeSet projectsBelongedTo copyWithout: self) isEmpty])
		ifTrue:
			[itsName _ changeSet name.
			ChangeSorter removeChangeSet: changeSet.
			Transcript cr; show: 'project change set ', itsName, ' deleted.'].

	^ true
