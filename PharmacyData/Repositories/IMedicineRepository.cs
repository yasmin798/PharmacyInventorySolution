using System;
using System.Collections.Generic;
using System.Text;

using PharmacyData.Entities;

namespace PharmacyData.Repositories
{
    public interface IMedicineRepository
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<Medicine?> GetByIdAsync(int id);
        Task AddAsync(Medicine medicine);
        Task UpdateAsync(Medicine medicine);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}