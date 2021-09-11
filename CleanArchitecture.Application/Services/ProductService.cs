using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        private readonly IMapper _mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _productRepository = productRepository ??
                 throw new ArgumentNullException(nameof(productRepository));

            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsEntity = await _productRepository.SelectAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var productEntity = await _productRepository.SelectAsync(id.Value);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        {
            var productEntity = await _productRepository.SelectEagerAsync(id.Value);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task AddAsync(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.InsertAsync(productEntity);
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {

            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(productEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            await _productRepository.DeleteAsync(id.Value);
        }
    }
}
