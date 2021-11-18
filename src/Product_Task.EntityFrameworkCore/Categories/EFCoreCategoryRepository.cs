using Microsoft.EntityFrameworkCore;
using Product_Task.EntityFrameworkCore;
using Product_Task.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Product_Task.Categories
{
    public class EFCoreCategoryRepository
         : EfCoreRepository<Product_TaskDbContext, Category, Guid>,
            ICategoryRepository
    {
        public EFCoreCategoryRepository(
            IDbContextProvider<Product_TaskDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<Category> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(category => category.Name == name);
        }

        public async Task<List<Category>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    category => category.Name.Contains(filter)
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    

    }
}
