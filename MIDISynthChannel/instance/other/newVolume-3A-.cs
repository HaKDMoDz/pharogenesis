newVolume: valueByte
	"Set the channel volume to the level given by the given number in the range 0..127."

	| snd newVolume |
	channelVolume := valueByte asFloat / 127.0.
	newVolume := masterVolume * channelVolume.
	activeSounds do: [:entry |
		snd := entry at: 2.
		snd adjustVolumeTo: newVolume overMSecs: 10].
