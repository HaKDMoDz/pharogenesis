testInstructions
	"just print all of methods of Object and see if no error accours"

	| printer  |

	printer  := InstructionPrinter.	

	Object methods do: [:method |
					self shouldnt: [ 
						String streamContents: [:stream | 
							(printer on: method) printInstructionsOn: stream]] raise: Error.
			].
