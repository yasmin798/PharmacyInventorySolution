using System;
using System.Collections.Generic;
using System.Text;

using PharmacyBusiness.DTOs;
using PharmacyData.Entities;
using PharmacyData.Repositories;

namespace PharmacyBusiness.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<IEnumerable<MedicineDto>> GetAllAsync()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            return medicines.Select(MapToDto);
        }

        public async Task<MedicineDto?> GetByIdAsync(int id)
        {
            var medicine = await _medicineRepository.GetByIdAsync(id);
            return medicine == null ? null : MapToDto(medicine);
        }

        public async Task<MedicineDto> CreateAsync(CreateMedicineDto dto)
        {
            var medicine = new Medicine
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                ExpiryDate = dto.ExpiryDate,
                CategoryId = dto.CategoryId,
                SupplierId = dto.SupplierId
            };

            await _medicineRepository.AddAsync(medicine);

            // Re-fetch with includes so Category/Supplier names are populated
            var created = await _medicineRepository.GetByIdAsync(medicine.Id);
            return MapToDto(created!);
        }

        public async Task<bool> UpdateAsync(int id, CreateMedicineDto dto)
        {
            var existing = await _medicineRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.StockQuantity = dto.StockQuantity;
            existing.ExpiryDate = dto.ExpiryDate;
            existing.CategoryId = dto.CategoryId;
            existing.SupplierId = dto.SupplierId;

            await _medicineRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exists = await _medicineRepository.ExistsAsync(id);
            if (!exists) return false;

            await _medicineRepository.DeleteAsync(id);
            return true;
        }

        private static MedicineDto MapToDto(Medicine medicine)
        {
            return new MedicineDto
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Description = medicine.Description,
                Price = medicine.Price,
                StockQuantity = medicine.StockQuantity,
                ExpiryDate = medicine.ExpiryDate,
                CategoryName = medicine.Category?.Name ?? "",
                SupplierName = medicine.Supplier?.Name ?? ""
            };
        }
    }
}
