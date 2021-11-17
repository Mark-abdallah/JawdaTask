using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Product_Task.Products
{
    public class CreateUpdateProductDto
    {
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
