using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Task.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product_Task.Web.Pages.Product
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<SelectListItem> Categories { get; set; }
        private readonly IProductAppService _productAppService;
        public IndexModel(IProductAppService productAppService)
        {
            _productAppService= productAppService;  
        }
        public async Task OnGetAsync()
        {
            var categoryLookUp = await _productAppService.GetCategoryLookUpAsync();
             Categories = categoryLookUp.Items
                  .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            Categories.Add(new SelectListItem { Text = "All" });
            ViewData["Categories"] = Categories;
        }
        public class CategoryFilter
        {
            [SelectItems(nameof(Categories))]
            [DisplayName("Category")]
            public Guid CategoryId { get; set; }
        }
    }
}