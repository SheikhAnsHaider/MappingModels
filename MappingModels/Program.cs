using MappingModels;

// Starting Point of the Application Here you can set the JSON file names to be read and processed.
// and Pass the Source and Target Model Types.
// This is just a simple Console Application to demonstrate the Mapping of Models.
// This will simply Read JSON file from the default directory where the application is running
// The 3rd Party Library which i used for Mapping is Mapperly, More information about their Documentation can be found
// on this link. https://mapperly.riok.app/docs/intro/ 
Console.WriteLine("Started...");

string FileName = "DIRS21.json";
//FileName = "Google.json";

if (File.Exists(FileName))
{
    string jsonContent = File.ReadAllText(FileName);
    if (jsonContent != string.Empty)
        MapHandler.Map(jsonContent, "DIRS21", "Google");
        //MapHandler.Map(jsonContent, "Google", "DIRS21");
}
else
{
    Console.WriteLine("File not found.");
    return;
}

Console.WriteLine("Finished.");
Console.ReadLine();
