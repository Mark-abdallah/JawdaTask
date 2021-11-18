using Product_Task.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Product_Task.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(Product_TaskEntityFrameworkCoreModule),
        typeof(Product_TaskApplicationContractsModule)
        )]
    public class Product_TaskDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
