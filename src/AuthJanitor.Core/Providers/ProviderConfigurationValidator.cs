using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthJanitor.Providers
{
    public class ProviderConfigurationValidator : IProviderConfigurationValidator
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

        public static IAuthJanitorProviderConfiguration CreateProviderConfiguration(Type providerConfigurationType, string jsonString)
        {
            try
            {
                return (IAuthJanitorProviderConfiguration)
                    JsonSerializer.Deserialize(jsonString, providerConfigurationType, SerializerOptions);
            }
            catch { return null; }
        }

        public interface IAuthJanitorProviderConfiguration
        {
            public bool IsConfigurationValid();
        }
    }
}
