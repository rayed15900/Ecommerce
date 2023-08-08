﻿using BusinessLogic.DTOs.ProductDTOs;
using BusinessLogic.IServices.Base;
using Models;

namespace BusinessLogic.IServices
{
    public interface IProductService : IService<ProductCreateDTO, ProductReadDTO, ProductUpdateDTO, Product>
    {
        Task<ProductCreateDTO> ProductCreateAsync(ProductCreateDTO dto);
        Task<ProductUpdateDTO> ProductUpdateAsync(ProductUpdateDTO dto);
        Task<ProductDetailDTO> ProductDetailAsync(int id);
    }
}
