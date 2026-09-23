using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;

namespace PharmacyBusiness.Services
{
    public interface IMedicineService
    {
        Task<IEnumerable<MedicineDto>> GetAllAsync();
        Task<MedicineDto?> GetByIdAsync(int id);
        Task<MedicineDto> CreateAsync(CreateMedicineDto dto);
        Task<bool> UpdateAsync(int id, CreateMedicineDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
