using Product_Task.Categories;
using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product_Task.Products
{
    public class Category: AuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }

        private Category()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Category(
            Guid id,
            [NotNull] string name )
            : base(id)
        {
            SetName(name);
          
        }

        internal Category ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: CategoryConsts.MaxNameLength
            );
        }

    }
}
