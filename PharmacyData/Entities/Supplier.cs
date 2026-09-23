using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace PharmacyData.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? ContactInfo { get; set; }

        public ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}