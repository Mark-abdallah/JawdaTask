using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Task.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product_Task.Web.Pages.Product
{
    public class CreateModalModel : Product_TaskPageModel
    {
        [BindProperty]
        public CreateProductViewModel Product { get; set; }

        public List<SelectListItem> Categories { get; set; }

        private readonly IProductAppService _productAppService;

        public CreateModalModel(
            IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

       

        public async Task OnGetAsync()
        {
            Product = new CreateProductViewModel();

            var categoryLookUp = await _productAppService.GetCategoryLookUpAsync();
            Categories = categoryLookUp.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            ViewData["Categories"] = Categories;
        }
        public async Task<IActionResult> OnPostAsync()

        {
            var dto = ObjectMapper.Map<CreateProductViewModel,CreateUpdateProductDto>(Product);
            await _productAppService.CreateAsync(dto);
            return NoContent();
        }




        public class CreateProductViewModel
        {
            [SelectItems(nameof(Categories))]
            [DisplayName("Category")]
            public Guid CategoryId { get; set; }

            [Required]
            [StringLength(40)]
            public string Name { get; set; }


            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishedAt { get; set; } = DateTime.Now;
            [Required]
            public IFormFile Image { get; set; }

            [Required]
            public float Price { get; set; }
            [Required]
            public int Stock { get; set; }
        }
    }
}