using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace PharmacyBusiness.DTOs
{
    public class CreateOrderItemDto
    {
        [Required]
        public int MedicineId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Order must contain at least one item")]
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}