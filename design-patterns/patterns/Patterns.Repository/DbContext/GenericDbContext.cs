using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Templates.Patterns.Repository
{
    public class GenericDbContext<T> : DbContext, IGenericDbContext<T> where T : class
    {
        public GenericDbContext(DbContextOptions<GenericDbContext<T>> options)
            : base(options)
        { }

        public virtual DbSet<T> Items { get; set; }

        public string GetConnectionFromAzureSettings(string configKey)
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration(options =>
                            options
                                .Connect(Environment.GetEnvironmentVariable("GTC_SHARED_CONNECTION"))
                                .ConfigureRefresh(refresh =>
                                {
                                    refresh.Register("Gtc:Shared:Sentinel", refreshAll: true)
                                           .SetCacheExpiration(new TimeSpan(0, 60, 0));
                                })
                                .Select(KeyFilter.Any, LabelFilter.Null)
                                .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production")
                        );
            var config = builder.Build();

            return config[configKey];
        }
    }
}
