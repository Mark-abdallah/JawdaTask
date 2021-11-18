using Volo.Abp.Modularity;

namespace Product_Task
{
    [DependsOn(
        typeof(Product_TaskApplicationModule),
        typeof(Product_TaskDomainTestModule)
        )]
    public class Product_TaskApplicationTestModule : AbpModule
    {

    }
}