//---------------------------------------------------------------------
// This script generates list of resource string names for C# class
// from the given text resource file where the data is represented
// as name value pares : name=value.
// It searches C# file for region : "#region Resource String Names"
// and replaces it with the generated list of names.
//
// (C) 2004 Vladimir Morozov (vmoroz@hotmail.com)
//
//---------------------------------------------------------------------

//---------------------------------------------------------------------
// Constant declarations
//---------------------------------------------------------------------

// File Header
var utilityName = "GenResNm.js";
var utilityInfo = utilityName
	+ ", Generates resource names from text (*.txt) string resource file.\n"
	+ "(c) 2004 Vladimir Morozov (vmoroz@hotmail.com)\n";
WScript.Echo(utilityInfo);

// Strings to match in C# file
var startRegion = "#region Resource String Names";
var endRegion = "#endregion";

// TextStream constants
var ForReading = 1, ForWriting = 2, ForAppending = 8;
var TristateUseDefault = -2, TristateTrue = -1, TristateFalse = 0;

//---------------------------------------------------------------------
// Read and check input parameters
//---------------------------------------------------------------------

// Check number of parameters
var unnamedArgs = WScript.Arguments.Unnamed;
Check(unnamedArgs.Length == 2, "Wrong number of arguments.");

// Check whether the given resource file exists
var fso = new ActiveXObject("Scripting.FileSystemObject");
var resFileName = unnamedArgs.Item(0);
Check(fso.FileExists(resFileName), "Resource file not found: '" + resFileName + "'.");
var resFile =  fso.GetFile(resFileName);

// Check whether the given C# file exists
var csFileName = unnamedArgs.Item(1);
Check(fso.FileExists(csFileName), "C# file not found: '" + csFileName + "'.");
var csFile =  fso.GetFile(csFileName);

// Show processing files
ShowProcessingFiles();

// Check modification date and exit if it up to date
if (resFile.DateLastModified <= csFile.DateLastModified)
{
	ReturnSuccess("C# file '" + csFileName + "' is up to date.");
}

//---------------------------------------------------------------------
// Create the new list of resource names and modify C# file.
//---------------------------------------------------------------------

// Create resource string name array
var names = GetResourceNames(resFile);

// Create new C# string with list of constants for resource names
var newResourceNames = startRegion + "\n\n";
for (var i = 0; i < names.length; i++)
{
	newResourceNames += "\t\t"
		+ "internal const string " + names[i] + " = \"" + names[i] + "\";"
		+ "\n\n";
}
newResourceNames += "\t\t"; //Leading tabs before #endregion

// Read C# file
var csText = ReadFile(csFile);

// Find and replace old resource name list with the new one
var startRegionIndex = csText.indexOf(startRegion);
Check(startRegionIndex > 0, "'" + startRegion + "' was not found in '" + csFileName + "'");
var endRegionIndex = csText.indexOf(endRegion, startRegionIndex);
Check(endRegionIndex > 0, "'" + endRegion + "' was not found in '" + csFileName + "'");
csText = csText.substr(0, startRegionIndex) + newResourceNames + csText.substr(endRegionIndex);

// Create the new C# file
WriteFile(csFile, csText);

// The resource name list was generated successfully
ReturnSuccess("File '" + csFileName + "' was updated.");

//---------------------------------------------------------------------
// Utility functions
//---------------------------------------------------------------------

// Show Error along with usage instruction and exit the script
function ReturnError(msg)
{
	var output = "Usage:\n";
	output += "csript.exe " + utilityName + " <txt resource> <cs file>\n";
	output += "  where\n";
	output += "  <txt resource> - full path to text resource file with name value pairs\n";
	output += "  <cs file> - file with custom resource manager file which has inside:\n";
	output += "              " + startRegion + "\n";
	if (msg)
	{
		output += "\nError: " + msg + "\n";
	}
	WScript.Echo(output);
	WScript.Quit(1);
}

// Show success message and exit the script
function ReturnSuccess(msg)
{
	var output = "";
	if (msg)
	{
		output += "Success: " + msg + "\n";
	}
	WScript.Echo(output);
	WScript.Quit(0);
}

// Check the value and return error message if it is false
function Check(value, msg)
{
	if (! value)
	{
		ReturnError(msg);
	}
}

// Show files being processed
function ShowProcessingFiles()
{
	var output = "Processing files:\n"
	output += "<txt resource> = '" + resFileName + "'\n";
	output += "<cs file> = '" + csFileName + "'\n";
	WScript.Echo(output);
}

// Returns array of all resource names from resource file
function GetResourceNames(resFile)
{
	var result = new Array();
	var regexp = /^(\w+)=/; // RegExp to match namein the beginning of string which
	                        // are terminated with '=' sign
	var stream = resFile.OpenAsTextStream(ForReading, TristateUseDefault);
	while (! stream.AtEndOfStream)
	{
		var s = stream.ReadLine();
		var m = regexp.exec(s);
		if (m)
		{
			result.push(m[1]);
		}
	}
	stream.Close();
	return result;
}

// Reads the file and returns its content
function ReadFile(file)
{
	var stream = file.OpenAsTextStream(ForReading, TristateUseDefault);
	var result = stream.ReadAll();
	stream.Close();
	return result;
}

// Saves the value string to the file
function WriteFile(file, value)
{
	var stream = file.OpenAsTextStream(ForWriting, TristateUseDefault);
	stream.Write(value);
	stream.Close();
}

