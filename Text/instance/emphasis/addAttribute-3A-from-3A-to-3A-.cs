addAttribute: att from: start to: stop 
	"Set the attribute for characters in the interval start to stop."
	runs _  runs copyReplaceFrom: start to: stop
			with: ((runs copyFrom: start to: stop)
				mapValues:
				[:attributes | Text addAttribute: att toArray: attributes])
