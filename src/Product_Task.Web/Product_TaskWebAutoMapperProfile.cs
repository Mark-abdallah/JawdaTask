using AutoMapper;
using Product_Task.Categories;
using Product_Task.Products;

namespace Product_Task.Web
{
    public class Product_TaskWebAutoMapperProfile : Profile
    {
        public Product_TaskWebAutoMapperProfile()
        {
            CreateMap<ProductDto, CreateUpdateProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Pages.Product.CreateModalModel.CreateProductViewModel, CreateUpdateProductDto>();

            CreateMap<ProductDto, Pages.Product.EditModalModel.EditProductViewModel>();

            CreateMap<Pages.Product.EditModalModel.EditProductViewModel, CreateUpdateProductDto>();

        }
    }
}
