allTypes

	^MessageTypes ifNil: [
		MessageTypes := {
			self typeKeyboardChat.
			self typeMorph.
			self typeFridge.
			self typeStatusRequest.
			self typeStatusReply.
			self typeSeeDesktop.
			self typeAudioChat.
			self typeAudioChatContinuous.
			self typeMultiChat.
		}
	]
