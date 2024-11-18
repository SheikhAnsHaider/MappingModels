using MappingModels.BusinessLogic;
using MappingModels.Models.Source.DIRS21;
using MappingModels.Models.Target.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MappingModels
{
    /// <summary>
    /// This Class is responsible for Mapping the Models from one type to another. based on the given instructions on Call.
    /// </summary>
    public static class MapHandler
    {
        public static void Map(string data, string sourceType, string targetType) 
        {
            switch (sourceType)
            {
                case "DIRS21":
                    switch (targetType)
                    {
                        case "Google":
                            // Mapping from DIRS21 to Google
                            // JsonProcessor has another Method which can return data in the form of Collection Model.
                            // For that will have to Create List<ClassModel> Object and call it's Method name ConvertStrictCollection
                            DIRS21Model source = JsonProcessor.ConvertStrict<DIRS21Model>(data);
                            var googleModel = ModelMapper.MapToGoogleModel(source);
                            // Just in case you want to Serialize the GoogleModel to JSON and write it to a file
                            string json = JsonSerializer.Serialize(googleModel);
                            File.WriteAllBytes("OutputConvertedGoogle.json", Encoding.UTF8.GetBytes(json));
                            break;
                        case "Expedia":
                            // Mapping from DIRS21 to Expedia
                            break;
                        default:
                            Console.WriteLine("Target type not found.");
                            break;
                    }
                    break;
                case "Google":
                    switch (targetType)
                    {
                        case "DIRS21":
                            // Mapping from Google to DIRS21
                            GoogleModel source = JsonProcessor.ConvertStrict<GoogleModel>(data);
                            var dirs21Model = ModelMapper.MapToDRIS21Model(source);
                            // Just in case you want to Serialize the GoogleModel to JSON and write it to a file
                            string json = JsonSerializer.Serialize(dirs21Model);
                            File.WriteAllBytes("OutputConvertedDIRS21.json", Encoding.UTF8.GetBytes(json));
                            break;
                        case "Expedia":
                            // Mapping from Google to Expedia
                            break;
                        default:
                            Console.WriteLine("Target type not found.");
                            break;
                    }
                    break;
                case "Expedia":
                    switch (targetType)
                    {
                        case "DIRS21":
                            // Mapping from Expedia to DIRS21
                            break;
                        case "Google":
                            // Mapping from Expedia to Google
                            break;
                        default:
                            Console.WriteLine("Target type not found.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("Source type not found.");
                    break;
            }
        }
    }
}
