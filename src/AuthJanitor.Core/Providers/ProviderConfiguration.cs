using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthJanitor.Providers
{
    public class ProviderConfiguration : IProviderConfiguration
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            IgnoreNullValues = true,            
            Converters = { new JsonStringEnumConverter() }
        };

        public bool IsConfigurationValid(LoadedProviderMetadata metadata, string jsonString)
        {
            try
            {
                var obj = JsonSerializer.Deserialize(jsonString, metadata.ProviderConfigurationType, SerializerOptions);
                return obj != null;
            }
            catch { return false; }
        }
    }
}
