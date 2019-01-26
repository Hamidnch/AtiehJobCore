using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AtiehJobCore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// Dynamically register all IEntityTypeConfiguration from specific assemblies with Reflection
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="assemblies">Assemblies contains Entities</param>
        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic &&
                            c.GetInterfaces()
                                .Any(i => i.IsConstructedGenericType &&
                                          i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var type in types)
            {
                //Because ApplyConfiguration is a Generic Method and dose not accept object
                dynamic configuration = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configuration);
            }
        }
    }
}
