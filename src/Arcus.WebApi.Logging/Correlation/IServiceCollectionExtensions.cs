﻿using System;
using Arcus.Observability.Correlation;
using GuardNet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Arcus.WebApi.Logging.Correlation 
{
    /// <summary>
    /// Adds operation and transaction correlation to the application.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds operation and transaction correlation to the application.
        /// </summary>
        /// <param name="services">The services collection containing the dependency injection services.</param>
        /// <param name="configureOptions">The function to configure additional options how the correlation works.</param>
        public static IServiceCollection AddHttpCorrelation(
            this IServiceCollection services, 
            Action<CorrelationInfoOptions> configureOptions = null)
        {
            Guard.NotNull(services, nameof(services));

            services.AddHttpContextAccessor();
            services.AddCorrelation(
                serviceProvider => new HttpCorrelationInfoAccessor(serviceProvider.GetRequiredService<IHttpContextAccessor>()), 
                configureOptions);

            return services;
        }
    }
}