using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace PharmacyBusiness.DTOs
{
    public class CreateSupplierDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? ContactInfo { get; set; }
    }
}
