﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AuthJanitor.Providers
{

    public class ProviderManagerService : IProviderStore
    {
        private readonly IServiceProvider _serviceProvider;

        public ProviderManagerService(
            IServiceProvider serviceProvider,
            IReadOnlyList<LoadedProviderMetadata> loadedProviders)
        {
            _serviceProvider = serviceProvider;
            LoadedProviders = loadedProviders; 
        }

        public bool HasProvider(ProviderIdentifier providerId) => LoadedProviders.Any(p => p.Id == providerId);

        public IReadOnlyList<LoadedProviderMetadata> LoadedProviders { get; }

        public LoadedProviderMetadata GetProviderMetadata(ProviderIdentifier providerId)
        {
            if (!HasProvider(providerId))
                throw new Exception($"Provider '{providerId}' not available!");
            else
                return LoadedProviders.First(p => p.Id == providerId);
        }

        public IAuthJanitorProvider GetProviderInstance(ProviderIdentifier providerId)
        {
            var metadata = GetProviderMetadata(providerId);
            return ActivatorUtilities.CreateInstance(_serviceProvider, metadata.ProviderType) as IAuthJanitorProvider;
        }

        public IAuthJanitorProvider GetProviderInstance(ProviderIdentifier providerId, string serializedProviderConfiguration)
        {
            var instance = GetProviderInstance(providerId);
            instance.SerializedConfiguration = serializedProviderConfiguration;
            return instance;
        }

        //It should return a Interface
        public AuthJanitorProviderConfiguration GetProviderConfiguration(ProviderIdentifier providerId) => ActivatorUtilities.CreateInstance(_serviceProvider, GetProviderMetadata(providerId).ProviderConfigurationType) as AuthJanitorProviderConfiguration;
        
        public AuthJanitorProviderConfiguration GetProviderConfiguration(ProviderIdentifier providerId, string serializedConfiguration) => JsonSerializer.Deserialize(serializedConfiguration, GetProviderMetadata(providerId).ProviderConfigurationType, ProviderConfiguration.SerializerOptions) as AuthJanitorProviderConfiguration;
        
    }

}

