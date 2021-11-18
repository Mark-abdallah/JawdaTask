using System;
using Volo.Abp.Application.Dtos;

namespace Product_Task.Products
{
    public class CategoryLookUpDto:EntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
