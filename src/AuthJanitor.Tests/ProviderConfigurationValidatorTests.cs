using AuthJanitor.Providers;
using AuthJanitor.Providers.AppServices;
using System;
using Xunit;

namespace AuthJanitor.Tests
{
    public class ProviderConfigurationValidatorTests
    {
        private readonly IProviderConfigurationValidator providerConfigurationValidator;

        public ProviderConfigurationValidatorTests() {
            providerConfigurationValidator = new ProviderConfigurationValidator();
        }

        [Fact]        
        public void ReceivingNullMustReturnFalse()
        {
            Assert.False(providerConfigurationValidator.IsConfigurationValid(null, null));
            Assert.False(providerConfigurationValidator.IsConfigurationValid(null, "{ \"Id\":\"Test\" }"));
        }

        [Fact]
        public void ReturnTrueWhenReceivingValidConfiguraiton()
        {
            var providerMetadata = new LoadedProviderMetadata()
            {
                ProviderConfigurationType = typeof(AppSettingConfiguration),
            };

            Assert.True(providerConfigurationValidator.IsConfigurationValid(providerMetadata, "{ \"SettingName\":\"Test\", \"CommitAsConnectionString\":true }"));
            Assert.True(providerConfigurationValidator.IsConfigurationValid(providerMetadata, "{ \"settingName\":\"Test\", \"commitAsConnectionString\":true }"));
            Assert.True(providerConfigurationValidator.IsConfigurationValid(providerMetadata, "{ \"settingname\":\"Test\", \"COMMITASCONNECTIONSTRING\":true }"));
        }



    }
}
