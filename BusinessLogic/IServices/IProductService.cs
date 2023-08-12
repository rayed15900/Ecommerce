using Models;
using BusinessLogic.IServices.Base;
using BusinessLogic.DTOs.ProductDTOs;

namespace BusinessLogic.IServices
{
    public interface IProductService : IService<ProductCreateDTO, ProductReadAllDTO, ProductUpdateDTO, Product>
    {
        Task<ProductCreateDTO> ProductCreateAsync(ProductCreateDTO dto);
        Task<ProductReadByIdDTO> ProductReadByIdAsync(int id);
        Task<ProductUpdateDTO> ProductUpdateAsync(ProductUpdateDTO dto);
        Task<Product> ProductDeleteAsync(int id);
    }
}
