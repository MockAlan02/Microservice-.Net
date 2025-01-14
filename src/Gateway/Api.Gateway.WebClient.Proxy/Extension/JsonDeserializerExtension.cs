using System.Text.Json;

namespace Api.Gateway.WebClient.Proxy.Extension
{
    public static class JsonDeserializerExtension
    {

        private static JsonSerializerOptions defaultSerializerSettings =
            new();

        // set this up how you need to!
        private static JsonSerializerOptions featureXSerializerSettings =
            new();


        public static T Deserialize<T>(this string json)
        {
            defaultSerializerSettings.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings)!;
        }

        public static T DeserializeCustom<T>(this string json, JsonSerializerOptions settings)
        {
            return JsonSerializer.Deserialize<T>(json, settings)!;
        }

        public static T DeserializeFeatureX<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, featureXSerializerSettings)!;
        }
    }

}
