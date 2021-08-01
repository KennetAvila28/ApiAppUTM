using System.Text.Json;
namespace AppUTM.Extensions
{
    //create function to deserialize a string to a JsonElement
    public static class StringToJsonElement
    {
        public static JsonElement Convert(string input)
        {
            JsonElement jsonElement = new JsonElement();
            jsonElement = JsonSerializer.Deserialize<JsonElement>(input.Replace("[", "").Replace("]", ""));
            //convierte el string a un JsonDocument
            var doc = JsonDocument.Parse(jsonElement.ToString());
            //obtiene el JsonElement del JsonDocumentaa
            return doc.RootElement;
        }
    }
}
