script63
	"restore canUnderstand: + introduce canPerform:"
	
| names | 
names := '
39Deprecated-md.11.mcz
Balloon-mir.11.mcz
Flash-md.3.mcz
TrueType-md.2.mcz
Collections-sd.63.mcz
CollectionsTests-sd.27.mcz
Compiler-sd.48.mcz 
Compression-stephaneducasse.4.mcz
Exceptions-md.7.mcz
EToys-md.20.mcz
FFI-CdG.4.mcz
Files-md.16.mcz
FlexibleVocabularies-md.4.mcz
Graphics-md.35.mcz
GraphicsTests-ar.8.mcz
Kernel-sd.125.mcz
KernelTests-sd.37.mcz
Morphic-sd.96.mcz
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
OB-Standard-sd.106.mcz
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
SUnitGUI-sd.7.mcz
System-sd.84.mcz
Tests-md.17.mcz
ToolBuilder-Kernel-sd.15.mcz
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

self halt.

	self loadTogether: names merge: true.
