using Volo.Abp.Application.Dtos;

namespace Product_Task.Categories
{
    public  class GetCategoryListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
