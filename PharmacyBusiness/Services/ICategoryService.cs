using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;

namespace PharmacyBusiness.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
