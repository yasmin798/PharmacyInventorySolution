using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyBusiness.DTOs
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
    }
}
