using System;
using System.Collections.Generic;
using System.Text;

using PharmacyData.Entities;

namespace PharmacyData.Repositories
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
