parseTokens: tokenStream
	|  entityStack document head token matchesAnything entity body |

	entityStack _ OrderedCollection new.

	"set up initial stack"
	document _ HtmlDocument new.
	entityStack add: document.
	
	head _ HtmlHead new.
	document addEntity: head.
	entityStack add: head.


	"go through the tokens, one by one"
	[ token _ tokenStream next.  token = nil ] whileFalse: [
		(token isTag and: [ token isNegated ]) ifTrue: [
			"a negated token"
			(token name ~= 'html' and: [ token name ~= 'body' ]) ifTrue: [
				"see if it matches anything in the stack"
				matchesAnything _ (entityStack detect: [ :e | e tagName = token name ] ifNone: [ nil ]) isNil not.
				matchesAnything ifTrue: [
					"pop the stack until we find the right one"
					[ entityStack last tagName ~= token name ] whileTrue: [ entityStack removeLast ].
					entityStack removeLast.
				]. ] ]
		ifFalse: [
			"not a negated token.  it makes its own entity"
			token isComment ifTrue: [
				entity _ HtmlCommentEntity new initializeWithText: token source.
			].
			token isText ifTrue: [
				entity _ HtmlTextEntity new text: token text.
				(((entityStack last shouldContain: entity) not) and: 
					[ token source isAllSeparators ]) ifTrue: [
					"blank text may never cause the stack to back up"
					entity _ HtmlCommentEntity new initializeWithText: token source ].
			].
			token isTag ifTrue: [
				entity _ token entityFor.
				entity = nil ifTrue: [ entity _ HtmlCommentEntity new initializeWithText: token source ] ].
			(token name = 'body')
				ifTrue: [body ifNotNil: [document removeEntity: body].
					body _ HtmlBody new initialize: token.
					document addEntity: body.
					entityStack add: body].

			entity = nil ifTrue: [ self error: 'could not deal with this token' ].

			entity isComment ifTrue: [
				"just stick it anywhere"
				entityStack last addEntity: entity ]
			ifFalse: [
				"only put it in something that is valid"
				[ entityStack last mayContain: entity ] 
					whileFalse: [ entityStack removeLast ].

				"if we have left the head, create a body"					
				(entityStack size < 2 and: [body isNil]) ifTrue: [
					body _ HtmlBody new.
					document addEntity: body.
					entityStack add: body  ].

				"add the entity"
				entityStack last addEntity: entity.
				entityStack addLast: entity.
			].
		]].

	body == nil ifTrue: [
		"add an empty body"
		body _ HtmlBody new.
		document addEntity: body ].

	document parsingFinished.

	^document