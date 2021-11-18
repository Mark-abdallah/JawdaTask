using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Product_Task.Data
{
    /* This is used if database provider does't define
     * IProduct_TaskDbSchemaMigrator implementation.
     */
    public class NullProduct_TaskDbSchemaMigrator : IProduct_TaskDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}