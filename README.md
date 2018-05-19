# xcpng.example.plugin
Example Windows Forms Plugin for XCP-NG Center

## Compiling

To compile you need: XenServer-7.4.0-SDK from http://www.citrix.com/downloads/xenserver/

This plugin was tested with:

XenServer-7.4.0-SDK
Feb 28, 2018
SHA-256 - 9972618501a20f9ae82ab4f9ed43bd9bca1ffd29a609bbdb661a8e94b104f1a1

Simply put the "xcpng.example.plugin" folder to "XenServer-SDK\XenServer.NET\xcpng.example.plugin" and add it to the samples project ("XenSdkSample.sln").

You need to manually fix the path to ResGen.exe and AL.exe in the PostBuildEvent section of the "xcpng.example.plugin.csproj" file.

One way to find the path is to start the Visual Stuio 2017 Developer commandline environment and run:

where resgen.exe
where al.exe


## Installation

To manually "install" the plugin you need to create the following path:

XCP-NG-Center-Install-Path\Plugins\xcp-ng.org\xcpng.example.plugin\

Then copy the following compiled files to that directory:

CookComputing.XmlRpcV2.dll
xcpng.example.plugin.exe  
xcpng.example.plugin.resources.dll  
xcpng.example.plugin.xcplugin.xml  
XenServer.dll  

To use the plugin start XCP-NG Center.  
Right clicking a Host: the context menu should contain an entry named "XCP-NG Example Plugin Host"  
Right clicking a VM: the context menu should contain an entry named "XCP-NG Example Plugin VM"  
Top Menu "Server": should contain an entry named "XCP-NG Example Plugin Host"  
Top Menu "VM": should contain an entry named "XCP-NG Example Plugin VM"  
