removeAction: anAction
forEvent: anEventSelector

    self
        removeActionsSatisfying: [:action | action = anAction]
        forEvent: anEventSelector