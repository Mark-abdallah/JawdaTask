using Product_Task.Categories;
using Product_Task.Products;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Product_Task
{
    public class CategoriesDataSeederContributer : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<Category, Guid> _categoryRepsotiory;
        private readonly CategoryManager _categoryManager;




        public CategoriesDataSeederContributer( 
            IRepository<Product,Guid> productRepository,
            IRepository<Category,Guid> categoryRepository,
            CategoryManager categoryManager
            )
        {
            _productRepository = productRepository;
            _categoryRepsotiory = categoryRepository;
            _categoryManager = categoryManager; 
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productRepository.GetCountAsync() > 0)
            {
                return;
            }

            var mobiles = await _categoryRepsotiory.InsertAsync(
              await _categoryManager.CreateAsync(
                "Mobiles"
                   )
                );
            var laptops = await _categoryRepsotiory.InsertAsync(
               await _categoryManager.CreateAsync(
                       "Laptops"
                   )
                );

          
            await _productRepository.InsertAsync(
                new Product
                {
                    CategoryId = mobiles.Id,
                    Name = "Iphone 13 Pro max",
                    Image="mobiles.jpg",
                    PublishedAt = DateTime.Now,
                    Price = 200000
                },
                autoSave: true
            ) ;

            await _productRepository.InsertAsync(
                new Product
                {
                    CategoryId = laptops.Id,
                    Name = "Lenovo z50-70",
                    Image = "laptop.jpg",
                    PublishedAt = DateTime.Now,
                    Price = 16000
                },
                autoSave: true
            );
        }
    }
}
