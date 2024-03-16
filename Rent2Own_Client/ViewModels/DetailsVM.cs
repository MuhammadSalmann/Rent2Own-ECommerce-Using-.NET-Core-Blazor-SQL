using Rent2Own_Models;
using System.ComponentModel.DataAnnotations;

namespace Rent2Own_Client.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM() 
        {
            _ProductPrice = new();
            Count = 1;
        }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
        public int Count { get; set; } 
        [Required]
        public int SelectedProductPriceId { get; set; }
        public ProductPrice _ProductPrice { get; set; }
    }
}
