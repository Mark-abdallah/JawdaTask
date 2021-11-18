using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Task.Products;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product_Task.Web.Pages.Product
{
    public class EditModalModel : Product_TaskPageModel
    {
        [BindProperty]
        public EditProductViewModel Product { get; set; }

        public List<SelectListItem> Categories { get; set; }

        private readonly IProductAppService _productAppService;

        public EditModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var productDto = await _productAppService.GetAsync(id);
            ViewData["selectedProductImage"] = "/Images/"+productDto.Image;
            productDto.Image = null;
            Product = ObjectMapper.Map<ProductDto, EditProductViewModel>(productDto);

            var categoryLookUp = await _productAppService.GetCategoryLookUpAsync();
            Categories = categoryLookUp.Items
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();
            ViewData["Categories"] = Categories;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.UpdateAsync(
                Product.Id,
                ObjectMapper.Map<EditProductViewModel, CreateUpdateProductDto>(Product)
            );

            return NoContent();
        }

        public class EditProductViewModel
        {
            [HiddenInput]
            public Guid Id { get; set; }

            [SelectItems(nameof(Categories))]
            [DisplayName("Category")]
            public Guid CategoryId { get; set; }

            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            public IFormFile Image { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishedAt { get; set; } = DateTime.Now;

            [Required]
            public float Price { get; set; }
            [Required]
            public int Stock { get; set; }

        }
    }
}