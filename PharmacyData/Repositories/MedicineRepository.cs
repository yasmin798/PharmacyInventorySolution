using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using PharmacyData.Entities;

namespace PharmacyData.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly PharmacyDbContext _context;

        public MedicineRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(int id)
        {
            return await _context.Medicines
                .Include(m => m.Category)
                .Include(m => m.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Medicine medicine)
        {
            _context.Medicines.Update(medicine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Medicines.AnyAsync(m => m.Id == id);
        }
    }
}