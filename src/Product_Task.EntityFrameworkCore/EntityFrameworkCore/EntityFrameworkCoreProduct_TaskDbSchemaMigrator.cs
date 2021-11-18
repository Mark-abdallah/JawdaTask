using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product_Task.Data;
using Volo.Abp.DependencyInjection;

namespace Product_Task.EntityFrameworkCore
{
    public class EntityFrameworkCoreProduct_TaskDbSchemaMigrator
        : IProduct_TaskDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreProduct_TaskDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the Product_TaskDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<Product_TaskDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
