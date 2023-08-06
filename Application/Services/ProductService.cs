using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto product)
        {
            ToEntity(product, out Product entityToAdd);
            try
            {
                var result = await unitOfWork.Products.AddAsync(entityToAdd);
                return ToDto(result);
            }
            catch (Exception)
            {
                throw new Exceptions.ApplicationException("Error at {0}: Added failed", this.GetType().Name);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id)
        {
            var result = await unitOfWork.Products.GetAsync(expression: p => p.Id.Equals(id), pageSize: 1);
            if (result.Result.Count == 0)
                throw new KeyNotFoundException($"Cannot found Id: {id}");
            return ToDto(result.Result.First());
        }

        public async Task<PaginationResult<ProductDto>> GetProductsAsync(int pageSize = 0, int pageIndex = 0, bool takeAll = false)
        {
            var result = await unitOfWork.Products.GetAsync(pageSize: pageSize, pageIndex: pageIndex, isTakeAll: takeAll);

            return new PaginationResult<ProductDto>
            {
                PageIndex = pageIndex,
                Result = result.Result.Select(p => ToDto(p)).ToList(),
                TotalCount = result.TotalCount
            };
        }

        private ProductDto ToDto(Product entity)
        {
            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                ProductStatus = entity.ProductStatus,
                Quantity = entity.Quantity
            };
        }

        private void ToEntity(ProductDto dto, out Product entity)
        {
            entity = new Product();
            entity.Quantity = dto.Quantity;
            entity.Price = dto.Price;
            entity.Description = dto.Description;
            entity.ProductStatus = dto.ProductStatus;
        }

    }
}
