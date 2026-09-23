using System;
using System.Collections.Generic;
using System.Text;

using PharmacyData.Entities;

namespace PharmacyData.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(string userId);
        Task AddAsync(Order order);
        Task DeleteAsync(int id);
    }
}