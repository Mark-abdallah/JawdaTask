using JetBrains.Annotations;
using Product_Task.Products;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Product_Task.Categories
{
    public class CategoryManager :DomainService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateAsync(
            [NotNull] string name
            )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingCategory = await _categoryRepository.FindByNameAsync(name);
            if (existingCategory != null)
            {
                throw new CategoryAlreadyExistsException(name);
            }

            return new Category(
                GuidGenerator.Create(),
                name
            );
        }

        public async Task ChangeNameAsync(
            [NotNull] Category category,
            [NotNull] string newName)
        {
            Check.NotNull(category, nameof(category));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingCategory = await _categoryRepository.FindByNameAsync(newName);
            if (existingCategory != null && existingCategory.Id != category.Id)
            {
                throw new CategoryAlreadyExistsException(newName);
            }

            category.ChangeName(newName);
        }
    }

}

