using Microsoft.AspNetCore.Http;
using System;
using Volo.Abp.Application.Dtos;


namespace Product_Task.Products
{
    public class ProductDto :AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Image { get; set; }
        public DateTime PublishedAt { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
