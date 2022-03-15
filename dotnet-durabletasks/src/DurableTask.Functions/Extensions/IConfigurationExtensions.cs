using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DurableTask.Functions
{
    public static class IConfigurationExtensions
    {
        public static IConfigurationSection GetSectionFromAzure<T>(this IConfiguration item, string section)
        {
            var jsonBuilder = new StringBuilder("{");
            jsonBuilder.Append($"{section}:");
            jsonBuilder.Append("{");
            foreach (var prop in typeof(T).GetProperties())
            {
                jsonBuilder.Append($@"{prop.Name}:{item[prop.Name]}");
            }
            jsonBuilder.Append("}}");

            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(jsonBuilder.ToString())));

            var configuration = builder.Build();
            return configuration.GetSection(section);
        }
    }
}
