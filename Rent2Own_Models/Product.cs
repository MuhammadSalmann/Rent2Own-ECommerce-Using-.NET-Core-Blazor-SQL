using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShopFavorites { get; set; }
        public bool CustomerFavorites { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Model Cannot be Negative")]
        public int? Model { get; set; } = null;
        [Required]
        public string Company { get; set; }
        public string ImageUrl { get; set; } 
        [Range(1, int.MaxValue, ErrorMessage = "Please Select a Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public List<ProductPrice> ProductPrices { get; set; }
    }
}
