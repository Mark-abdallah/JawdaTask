using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Product_Task.Categories
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
