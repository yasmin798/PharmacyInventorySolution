using System;
using System.Collections.Generic;
using System.Text;

namespace PharmacyBusiness.DTOs
{
    public class OrderItemDto
    {
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
