using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Product_Task.Web
{
    [Dependency(ReplaceServices = true)]
    public class Product_TaskBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Product_Task";
    }
}
