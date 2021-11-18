using System;
using System.Collections.Generic;
using System.Text;
using Product_Task.Localization;
using Volo.Abp.Application.Services;

namespace Product_Task
{
    /* Inherit your application services from this class.
     */
    public abstract class Product_TaskAppService : ApplicationService
    {
        protected Product_TaskAppService()
        {
            LocalizationResource = typeof(Product_TaskResource);
        }
    }
}
