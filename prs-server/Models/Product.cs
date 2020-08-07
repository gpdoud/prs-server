using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
    public class Product {

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string PartNbr { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Price { get; set; }
        [Required]
        [StringLength(30)]
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public Product() { }
    }
}
