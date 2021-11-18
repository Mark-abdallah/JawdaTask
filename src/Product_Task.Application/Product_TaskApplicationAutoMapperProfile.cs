using AutoMapper;
using Product_Task.Categories;
using Product_Task.Products;

namespace Product_Task
{
    public class Product_TaskApplicationAutoMapperProfile : Profile
    {
        public Product_TaskApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<Category, CategoryDto>();
           // CreateMap<ProductDto, CreateUpdateProductDto>();
            CreateMap<Category, CategoryLookUpDto>();
          //  CreateMap<CreateProductViewModel, CreateUpdateProductDto>();

           

        }
    }
}
