using AutoMapper;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categoriesEntity = await _categoryRepository.SelectAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetByIdAsync(int? id)
        {
            var categoryEntity = await _categoryRepository.SelectAsync(id.Value);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task AddAsync(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.InsertAsync(categoryEntity);
        }

        public async Task UpdateAsync(CategoryDTO categoryDto)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task RemoveAsync(int? id)
        {
            var products = await _productRepository.SelectByCategoryIdAsync(id.Value);

            if (products == null || products.Count() == 0)
                await _categoryRepository.DeleteAsync(id.Value);
            else
                throw new Exception("category cannot be deleted, but it is necessary to delete the linked products the same");
        }
    }
}
