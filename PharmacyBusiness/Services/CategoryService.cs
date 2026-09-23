using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;
using PharmacyData.Entities;
using PharmacyData.Repositories;

namespace PharmacyBusiness.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return category == null ? null : new CategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await _categoryRepository.AddAsync(category);
            return new CategoryDto { Id = category.Id, Name = category.Name };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _categoryRepository.ExistsAsync(id);
            if (!exists) return false;

            await _categoryRepository.DeleteAsync(id);
            return true;
        }
    }
}
