AppResLib.dll Generator
=======================
This project with automatically generate AppResLib.dll and .mui files based on
resource files (.resx) in your Windows Phone 8 project. 
It will extract 3 strings from your .resx files:
- 100 from ApplicationDisplayName
- 101 from ApplicationDescription
- 102 from ApplicationTileTitle
These keys are configurable in AppResLib.targets file.

AppResLib.dll and .mui files will be included in your project output.

To use these localized strings, you will need to replace hard-coded strings in
your WMAppManifest.xml to @AppResLib.dll,-100 to use string extracted from
ApplicationDisplayName key.
You can also use @AppResLib.dll,-101 for Description and @AppResLib.dll,-102
for Tile Title.
