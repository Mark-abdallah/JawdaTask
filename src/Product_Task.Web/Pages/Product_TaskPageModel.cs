using Product_Task.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Product_Task.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class Product_TaskPageModel : AbpPageModel
    {
        protected Product_TaskPageModel()
        {
            LocalizationResourceType = typeof(Product_TaskResource);
        }
    }
}