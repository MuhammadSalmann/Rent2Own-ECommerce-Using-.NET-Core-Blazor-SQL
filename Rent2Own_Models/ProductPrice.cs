using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Models
{
    public class ProductPrice
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        //public Product Product { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Price Cannot be Negative")]
        public double Price { get; set; }
    }
}
