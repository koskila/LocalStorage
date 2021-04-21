﻿using Blazored.LocalStorage.JsonConverters;
using Blazored.LocalStorage.Serialization;
using Blazored.LocalStorage.StorageOptions;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Blazored.LocalStorage.Testing
{
    [ExcludeFromCodeCoverage]
    public static class BUnitLocalStorageTestExtensions
    {
        public static void AddBlazoredLocalStorage(this TestContextBase context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            context.Services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, InMemoryStorageProvider>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<ISyncLocalStorageService, LocalStorageService>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }

        public static void AddBlazoredLocalStorage(this TestContextBase context, Action<LocalStorageOptions> configure)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            context.Services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, InMemoryStorageProvider>()
                .AddScoped<ILocalStorageService, LocalStorageService>()
                .AddScoped<ISyncLocalStorageService, LocalStorageService>()
                .Configure<LocalStorageOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}
