using AuthJanitor.Providers;
using AuthJanitor.Providers.AppServices;
using System;
using Xunit;

namespace AuthJanitor.Tests
{
    public class ProviderConfigurationTests
    {
        private readonly IProviderConfiguration providerConfiguration;

        public ProviderConfigurationTests() {
            providerConfiguration = new ProviderConfiguration();
        }

        [Fact]        
        public void ReceivingNullMustReturnFalse()
        {
            Assert.False(providerConfiguration.IsConfigurationValid(null, null));
            Assert.False(providerConfiguration.IsConfigurationValid(null, "{ \"Id\":\"Test\" }"));
        }

        [Fact]
        public void ReturnTrueWhenReceivingValidConfiguraiton()
        {
            var providerMetadata = new LoadedProviderMetadata()
            {
                ProviderConfigurationType = typeof(AppSettingConfiguration),
            };

            Assert.True(providerConfiguration.IsConfigurationValid(providerMetadata, "{ \"SettingName\":\"Test\", \"CommitAsConnectionString\":true }"));
            Assert.True(providerConfiguration.IsConfigurationValid(providerMetadata, "{ \"settingName\":\"Test\", \"commitAsConnectionString\":true }"));
            Assert.True(providerConfiguration.IsConfigurationValid(providerMetadata, "{ \"settingname\":\"Test\", \"COMMITASCONNECTIONSTRING\":true }"));
        }



    }
}
