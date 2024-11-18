using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MappingModels.BusinessLogic
{
    /// <summary>
    /// This Class is responsible for processing JSON data and converting it to the specified model. It has only 1 Accessible Method
    /// which is ConvertStrict which takes JSON string and Model Type as input and returns the Model Object. and Also 2nd public Method
    /// Which is ConvertStrictToList which takes JSON string and Model Type as input and returns the List of Model Object.
    /// Both Methods can be used depending on the requirement.
    /// </summary>
    public static class JsonProcessor
    {
        public static T? ConvertStrict<T>(string json) where T : class
        {
            string ErrorMessage = "";
            try
            {
                // Parsing the JSON to extract properties here  
                var jsonProperties = ParseJsonProperties(json);
                if (jsonProperties.Count == 0)
                {
                    Console.WriteLine($"Error parsing JSON properties");
                    return null;
                }
                // Getting model properties here
                var modelProperties = typeof(T).GetProperties()
                                               .Select(p => p.Name)
                                               .ToHashSet(StringComparer.OrdinalIgnoreCase);

                // Finding unmatched properties here both in JSON-> Model and Model->JSON
                var unmatchedInJson = jsonProperties.Except(modelProperties).ToList();
                var unmatchedInModel = modelProperties.Except(jsonProperties).ToList();

                if (unmatchedInJson.Any() || unmatchedInModel.Any())
                {
                    var message = BuildMismatchMessage(unmatchedInJson, unmatchedInModel);
                    ErrorMessage = $"JSON does not completely match with the model.\n{message}";
                    Console.WriteLine(ErrorMessage);
                }

                // Parsing JSON and adjusting structure dynamically if needed
                var jsonDocument = JsonDocument.Parse(json);
                var adjustedJson = AdjustJsonStructureForModel<T>(jsonDocument.RootElement);

                // Deserialize the JSON into the model
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return JsonSerializer.Deserialize<T>(adjustedJson, options);
            }
            catch (Exception ex)
            {
                if (ErrorMessage == string.Empty)
                    Console.WriteLine($"Deserialization resulted in null. Error parsing JSON: {ex.Message}");
                return null;
            }
        }
        public static List<T>? ConvertStrictToList<T>(string json) where T : class
        {
            string ErrorMessage = "";
            try
            {
                // Parse the JSON to check if it is an array
                var jsonDocument = JsonDocument.Parse(json);

                if (jsonDocument.RootElement.ValueKind != JsonValueKind.Array)
                {
                    Console.WriteLine("JSON root element is not an array.");
                    return null;
                }

                var items = new List<T>();
                foreach (var element in jsonDocument.RootElement.EnumerateArray())
                {
                    // Extract properties from each object
                    var jsonObject = element.GetRawText();
                    var jsonProperties = ParseJsonProperties(jsonObject);

                    if (jsonProperties.Count == 0)
                    {
                        Console.WriteLine($"Error parsing JSON properties for an object in the array.");
                        continue;
                    }

                    // Get model properties
                    var modelProperties = typeof(T).GetProperties()
                                                   .Select(p => p.Name)
                                                   .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    // Find unmatched properties
                    var unmatchedInJson = jsonProperties.Except(modelProperties).ToList();
                    var unmatchedInModel = modelProperties.Except(jsonProperties).ToList();

                    if (unmatchedInJson.Any() || unmatchedInModel.Any())
                    {
                        var message = BuildMismatchMessage(unmatchedInJson, unmatchedInModel);
                        ErrorMessage = $"JSON does not completely match the model.\n{message}";
                        Console.WriteLine(ErrorMessage);
                    }

                    // Adjust JSON structure dynamically
                    var adjustedJson = AdjustJsonStructureForModel<T>(element);

                    // Deserialize the JSON into the model
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var deserializedObject = JsonSerializer.Deserialize<T>(adjustedJson, options);

                    if (deserializedObject != null)
                        items.Add(deserializedObject);
                }

                return items;
            }
            catch (Exception ex)
            {
                if (ErrorMessage == string.Empty)
                    Console.WriteLine($"Deserialization resulted in null. Error parsing JSON: {ex.Message}");
                return null;
            }
        }

        private static string AdjustJsonStructureForModel<T>(JsonElement jsonElement)
        {
            var modelProperties = typeof(T).GetProperties();
            var jsonObject = jsonElement.Clone();

            // Create a mutable dictionary from the JSON element
            var jsonDict = jsonObject.EnumerateObject().ToDictionary(p => p.Name, p => p.Value);

            foreach (var property in modelProperties)
            {
                var propName = property.Name;
                var propType = property.PropertyType;

                if (jsonDict.ContainsKey(propName))
                {
                    var jsonValue = jsonDict[propName];
                    var isCollection = typeof(IEnumerable<object>).IsAssignableFrom(propType) && propType != typeof(string);

                    if (isCollection && jsonValue.ValueKind == JsonValueKind.Object)
                    {
                        var array = new JsonElement[] { jsonValue };
                        jsonDict[propName] = CreateJsonArray(array);
                    }
                    else if (!isCollection && jsonValue.ValueKind == JsonValueKind.Array)
                    {
                        var firstElement = jsonValue.EnumerateArray().FirstOrDefault();
                        if (firstElement.ValueKind != JsonValueKind.Undefined && firstElement.ValueKind != JsonValueKind.Null)
                        {
                            jsonDict[propName] = firstElement;
                        }
                    }
                }
            }

            // Recreate the adjusted JSON string
            using (var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(jsonDict)))
            {
                return jsonDoc.RootElement.GetRawText();
            }
        }

        private static JsonElement CreateJsonArray(IEnumerable<JsonElement> elements)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartArray();
                    foreach (var element in elements)
                    {
                        element.WriteTo(writer);
                    }
                    writer.WriteEndArray();
                    writer.Flush();

                    stream.Position = 0;
                    using (var jsonDoc = JsonDocument.Parse(stream))
                    {
                        return jsonDoc.RootElement.Clone();
                    }
                }
            }
        }

        private static HashSet<string> ParseJsonProperties(string json)
        {
            try
            {
                using (var jsonDoc = JsonDocument.Parse(json))
                {
                    if (jsonDoc.RootElement.ValueKind != JsonValueKind.Object)
                    {
                        Console.WriteLine("JSON root element is not an object.");
                        return new HashSet<string>();
                    }
                    else
                    {
                        return jsonDoc.RootElement.EnumerateObject()
                                                   .Select(prop => prop.Name)
                                                   .ToHashSet(StringComparer.OrdinalIgnoreCase);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON properties: {ex.Message}");
                return new HashSet<string>();
            }
        }

        private static string BuildMismatchMessage(IEnumerable<string> unmatchedInJson, IEnumerable<string> unmatchedInModel)
        {
            try
            {
                var message = new List<string>();
                if (unmatchedInJson.Any())
                    message.Add($"Unmatched properties in JSON: {string.Join(", ", unmatchedInJson)}");

                if (unmatchedInModel.Any())
                    message.Add($"Missing properties in JSON: {string.Join(", ", unmatchedInModel)}");
                return string.Join("\n", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error building mismatch message: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
