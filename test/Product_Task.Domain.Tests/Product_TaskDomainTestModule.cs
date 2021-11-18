using Product_Task.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Product_Task
{
    [DependsOn(
        typeof(Product_TaskEntityFrameworkCoreTestModule)
        )]
    public class Product_TaskDomainTestModule : AbpModule
    {

    }
}