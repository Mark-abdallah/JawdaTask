using System;
using Volo.Abp.Application.Dtos;

namespace Product_Task.Categories
{
    public class CategoryDto :EntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
