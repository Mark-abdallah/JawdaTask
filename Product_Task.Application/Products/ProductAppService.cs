using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Product_Task.Categories;
using Product_Task.Permissions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Product_Task.Products
{
    
    public class ProductAppService :
        CrudAppService<
            Product,
            ProductDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDto>,
        IProductAppService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductAppService(IRepository<Product,Guid> repository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
            :base(repository)
        {
            _categoryRepository = categoryRepository;
            GetPolicyName = Product_TaskPermissions.Products.Default;
            GetListPolicyName = Product_TaskPermissions.Products.Default;
            CreatePolicyName = Product_TaskPermissions.Products.Create;
            UpdatePolicyName = Product_TaskPermissions.Products.Edit;
            DeletePolicyName = Product_TaskPermissions.Products.Create;
            _hostingEnvironment = webHostEnvironment;

        }

        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto dto)
        {
            string imageUrl = UploadImage(dto);

            var product = new Product
            {
                Name = dto.Name,
                Stock = dto.Stock,
                PublishedAt = dto.PublishedAt,
                CategoryId = dto.CategoryId,
                Price = dto.Price,
                Image = imageUrl

            };
            await Repository.InsertAsync(product);
            var mappedProduct = ObjectMapper.Map<Product, ProductDto>(product);
            return mappedProduct;
        }
        private string UploadImage(CreateUpdateProductDto productDto)
        {
            string fileName = null;
            if (productDto.Image != null)
            {
                string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + productDto.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    productDto.Image.CopyTo(fileStream);
                }

            }
            return fileName;
        } 

        public  override async Task<ProductDto> UpdateAsync(Guid id,CreateUpdateProductDto dto )
        {
            string imageUrl = UploadImage(dto);
            var wantedProduct = await Repository.FindAsync(id);
            wantedProduct.Name = dto.Name;
            wantedProduct.Price = dto.Price;
            wantedProduct.Stock = dto.Stock;
            wantedProduct.CategoryId=dto.CategoryId;
            wantedProduct.Image = imageUrl;
            var mappedProduct = ObjectMapper.
                Map<Product, ProductDto>(wantedProduct);
            await Repository.UpdateAsync(wantedProduct, true);
            return mappedProduct;
        }


        public override async Task<ProductDto> GetAsync(Guid id)
        {
            //Get the IQueryable<product> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join Products and Category
            var query = from product in queryable
                        join category in _categoryRepository on product.CategoryId equals category.Id
                        where product.Id == id
                        select new { product, category };

            //Execute the query and get the product with category
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Product), id);
            }

            var productDto = ObjectMapper.Map<Product, ProductDto>(queryResult.product);
            productDto.CategoryName = queryResult.category.Name;
            return productDto;
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //Get the IQueryable<product> from the repository
            var queryable = await Repository.GetQueryableAsync();

            //Prepare a query to join products and categories
            var query = from product in queryable
                        join category in _categoryRepository on product.CategoryId equals category.Id
                        select new { product, category };

            //Paging
            query = query
                .OrderBy(NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            //Execute the query and get a list
            var queryResult = await AsyncExecuter.ToListAsync(query);

            //Convert the query result to a list of productDto objects
            var productDtos = queryResult.Select(x =>
            {
                var productDto = ObjectMapper.Map<Product, ProductDto>(x.product);
                productDto.CategoryName = x.category.Name;
                return productDto;
            }).ToList();

            //Get the total count with another query
            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<ProductDto>(
                totalCount,
                productDtos
            );
        }
        [AllowAnonymous]
        public async Task<ListResultDto<CategoryLookUpDto>> GetCategoryLookUpAsync()
        {
            var Categories = await _categoryRepository.GetListAsync();

            return new ListResultDto<CategoryLookUpDto>(
                ObjectMapper.Map<List<Category>, List<CategoryLookUpDto>>(Categories)
            );
        }

        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"product.{nameof(Product.Name)}";
            }

            if (sorting.Contains("categoryName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "categoryName",
                    "category.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"product.{sorting}";
        }
    }
}
