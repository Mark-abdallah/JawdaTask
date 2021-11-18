using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product_Task.Products
{
    public  class Product : AuditedAggregateRoot<Guid>
    {
       // public  Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }


       
    }
}
