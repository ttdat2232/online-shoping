using Domain.Entities;
using Domain.Models;
using Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginationResult<ProductDto>> GetProductsAsync(int pageSize = 0, int pageIndex = 0, bool takeAll = false);
        Task<ProductDto> GetProductByIdAsync(Guid id);
        Task<ProductDto> AddProductAsync(ProductDto product);
    }
}
