using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;
using PharmacyData.Entities;
using PharmacyData.Repositories;

namespace PharmacyBusiness.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return suppliers.Select(s => new SupplierDto { Id = s.Id, Name = s.Name, ContactInfo = s.ContactInfo });
        }

        public async Task<SupplierDto?> GetByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            return supplier == null ? null : new SupplierDto { Id = supplier.Id, Name = supplier.Name, ContactInfo = supplier.ContactInfo };
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto dto)
        {
            var supplier = new Supplier { Name = dto.Name, ContactInfo = dto.ContactInfo };
            await _supplierRepository.AddAsync(supplier);
            return new SupplierDto { Id = supplier.Id, Name = supplier.Name, ContactInfo = supplier.ContactInfo };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _supplierRepository.ExistsAsync(id);
            if (!exists) return false;

            await _supplierRepository.DeleteAsync(id);
            return true;
        }
    }
}