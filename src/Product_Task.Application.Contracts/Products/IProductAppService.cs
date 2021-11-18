using Product_Task.Categories;
using Product_Task.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Product_Task.Products
{
    public interface IProductAppService :
        ICrudAppService<
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto>
    {
        Task<ListResultDto<CategoryLookUpDto>> GetCategoryLookUpAsync();
    }
}
