using Microsoft.AspNetCore.Authorization;
using Product_Task.Permissions;
using Product_Task.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Product_Task.Categories
{
   // [Authorize(Product_TaskPermissions.Categories.Default)]
   [AllowAnonymous]

    public class CategoryAppService : Product_TaskAppService, ICategoryAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryManager _categoryManager;

        public CategoryAppService(
            ICategoryRepository categoryRepository,
            CategoryManager categoryManager)
        {
            _categoryRepository = categoryRepository;
            _categoryManager = categoryManager;
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);
            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

        public async Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Category.Name);
            }

            var categories = await _categoryRepository.GetListAsync(
                input.SkipCount,
                input.MaxResultCount,
                input.Sorting,
                input.Filter
            );

            var totalCount = input.Filter == null
                ? await _categoryRepository.CountAsync()
                : await _categoryRepository.CountAsync(category => category.Name.Contains(input.Filter));

            return new PagedResultDto<CategoryDto>(
                totalCount,
                ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories)
            );
        }

      //  [Authorize(Product_TaskPermissions.Categories.Create)]
        public async Task<CategoryDto> CreateAsync(CreateCategoryDto input)
        {
            var category = await _categoryManager.CreateAsync(
                input.Name
            );

            await _categoryRepository.InsertAsync(category);

            return ObjectMapper.Map<Category, CategoryDto>(category);
        }

      //  [Authorize(Product_TaskPermissions.Categories.Edit)]
        public async Task UpdateAsync(Guid id, UpdateCategoryDto input)
        {
            var category = await _categoryRepository.GetAsync(id);

            if (category.Name != input.Name)
            {
                await _categoryManager.ChangeNameAsync(category, input.Name);
            }

            await _categoryRepository.UpdateAsync(category);
        }

//[Authorize(Product_TaskPermissions.Categories.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
