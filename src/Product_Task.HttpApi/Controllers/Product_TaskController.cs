using Product_Task.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Product_Task.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class Product_TaskController : AbpController
    {
        protected Product_TaskController()
        {
            LocalizationResource = typeof(Product_TaskResource);
        }


    }
}