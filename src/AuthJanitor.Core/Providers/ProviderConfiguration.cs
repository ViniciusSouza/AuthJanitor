using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthJanitor.Providers
{
    public class ProviderConfiguration : IProviderConfiguration
    {
        public static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = false,
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() }
        };

        public bool IsConfigurationValid(LoadedProviderMetadata metadata, string serializedConfiguration)
        {
            try
            {
                var obj = JsonSerializer.Deserialize(serializedConfiguration, metadata.ProviderConfigurationType, SerializerOptions);
                return obj != null;
            }
            catch { return false; }
        }
    }
}
