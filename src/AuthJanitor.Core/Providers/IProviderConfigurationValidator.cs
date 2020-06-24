using System;
using System.Collections.Generic;
using System.Text;

namespace AuthJanitor.Providers
{
    public interface IProviderConfigurationValidator
    {
        bool IsConfigurationValid(LoadedProviderMetadata metadata, string serializedConfiguration);
    }
}
