using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;

namespace PharmacyBusiness.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(string userId);
        Task<(bool Success, string? Error, OrderDto? Order)> CreateAsync(string userId, CreateOrderDto dto);
    }
}