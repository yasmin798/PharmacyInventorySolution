using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;
using PharmacyData.Entities;
using PharmacyData.Repositories;

namespace PharmacyBusiness.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMedicineRepository _medicineRepository;

        public OrderService(IOrderRepository orderRepository, IMedicineRepository medicineRepository)
        {
            _orderRepository = orderRepository;
            _medicineRepository = medicineRepository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Select(MapToDto);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetByUserIdAsync(userId);
            return orders.Select(MapToDto);
        }

        public async Task<(bool Success, string? Error, OrderDto? Order)> CreateAsync(string userId, CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;

            // Validate stock and build order items BEFORE saving anything
            foreach (var item in dto.Items)
            {
                var medicine = await _medicineRepository.GetByIdAsync(item.MedicineId);

                if (medicine == null)
                    return (false, $"Medicine with Id {item.MedicineId} not found.", null);

                if (medicine.StockQuantity < item.Quantity)
                    return (false, $"Not enough stock for '{medicine.Name}'. Available: {medicine.StockQuantity}", null);

                order.OrderItems.Add(new OrderItem
                {
                    MedicineId = medicine.Id,
                    Quantity = item.Quantity,
                    UnitPrice = medicine.Price
                });

                total += medicine.Price * item.Quantity;

                // Reduce stock
                medicine.StockQuantity -= item.Quantity;
                await _medicineRepository.UpdateAsync(medicine);
            }

            order.TotalAmount = total;

            await _orderRepository.AddAsync(order);

            var created = await _orderRepository.GetByIdAsync(order.Id);
            return (true, null, MapToDto(created!));
        }

        private static OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    MedicineId = oi.MedicineId,
                    MedicineName = oi.Medicine?.Name ?? "",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }
    }
}
