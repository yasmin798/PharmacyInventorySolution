using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;

namespace PharmacyBusiness.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDto>> GetAllAsync();
        Task<SupplierDto?> GetByIdAsync(int id);
        Task<SupplierDto> CreateAsync(CreateSupplierDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
