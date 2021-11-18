using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Product_Task.Web.Pages.Product
{
    public class ProductInfoModalModel : AbpPageModel
    {
        public string Name { get; set; }

        public double Price{ get; set; }

        public string Image { get; set; }

       
    }
}
