
using System;
using AspNetCore.DataProtection.EntityFramework;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureDataProtectionBuilderExtensions
    {
        /// <summary>
        /// Configures the data protection system to persist keys using the specified DbContext.
        /// </summary>
        /// <typeparam name="TContext">The DbContext type to use. Must implement IDataProtectionContext</typeparam>
        /// <param name="builder">The builder instance to modify</param>
        /// <returns>The value <paramref name="builder"/>.</returns>
        /// <remarks>
        /// The <typeparamref name="TContext"/> must be configured with EntityFramework using AddDbContext<TContext>
        /// </remarks>
        public static IDataProtectionBuilder PersistKeysToDatabase<TContext>(this IDataProtectionBuilder builder)
            where TContext : class, IDataProtectionContext
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddScoped<IDataProtectionContext, TContext>();

            return PersistKeysToDatabaseInternal(builder);
        }

        /// <summary>
        /// Configures the data protection system to persist keys to the specified database.
        /// </summary>        
        /// <param name="builder">The builder instance to modify</param>
        /// <param name="options">The callback for configuring the database options.</param>
        /// <returns>The value <paramref name="builder"/>.</returns>
        public static IDataProtectionBuilder PersistKeysToDatabase(this IDataProtectionBuilder builder, Action<DbContextOptionsBuilder> options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            builder.Services.AddDbContext<DataProtectionContext>(options);
            builder.Services.AddScoped<IDataProtectionContext, DataProtectionContext>();

            return PersistKeysToDatabaseInternal(builder);
        }

        private static IDataProtectionBuilder PersistKeysToDatabaseInternal(IDataProtectionBuilder config)
        {
            config.Services.AddTransient<IXmlRepository, EntityFrameworkXmlRepository>();
            return config;
        }
    }
}