script59
	
| names | 
names := '
39Deprecated-md.11.mcz
Balloon-mir.11.mcz
Flash-md.3.mcz
TrueType-md.2.mcz
Collections-md.60.mcz
CollectionsTests-md.24.mcz
Compiler-md.47.mcz 
Compression-stephaneducasse.4.mcz
Exceptions-md.7.mcz
EToys-md.20.mcz
FFI-CdG.4.mcz
Files-md.16.mcz
FlexibleVocabularies-md.4.mcz
Graphics-md.35.mcz
GraphicsTests-ar.8.mcz
Kernel-md.116.mcz
KernelTests-md.33.mcz
Morphic-md.93.mcz
MorphicExtras-md.27.mcz
MorphicTests-stephaneducasse.4.mcz
Monticello-al.295.mcz
MonticelloConfigurations-stephaneducasse.38.mcz
Movies-md.7.mcz
Multilingual-md.20.mcz
Nebraska-md.10.mcz
Network-md.27.mcz
NetworkTests-gk.8.mcz
OmniBrowser-lr.281.mcz
OB-Standard-lr.105.mcz
PackageInfo-stephaneducasse.5.mcz
ReleaseBuilder-md.4.mcz
PlusTools-md.34.mcz
PreferenceBrowser-md.30.mcz
Protocols-md.12.mcz
Services-Base-md.27.mcz
SMBase-md.72.mcz
SMLoader-md.29.mcz
SmaCC-fbs.8.mcz
Sound-md.6.mcz
ST80-md.33.mcz
StarSqueak-sd.6.mcz
SUnit-md.32.mcz
SUnitGUI-lr.6.mcz
System-md.83.mcz
Tests-md.17.mcz
ToolBuilder-Kernel-stephaneducasse.14.mcz
ToolBuilder-Morphic-stephaneducasse.16.mcz
ToolBuilder-MVC-dtl.12.mcz
ToolBuilder-SUnit-stephaneducasse.10.mcz
Tools-md.68.mcz
Traits-al.214.mcz
Speech-md.8.mcz
VersionNumber-dew.1.mcz
SystemChangeNotification-Tests-sd.5.mcz
FixUnderscores-md.9.mcz
FixInvisible-bf.1.mcz
'  
findTokens: ' ', String cr.

	self loadTogether: names merge: true.
