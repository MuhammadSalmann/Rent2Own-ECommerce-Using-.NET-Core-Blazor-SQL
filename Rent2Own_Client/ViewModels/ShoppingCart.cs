using Rent2Own_Models;

namespace Rent2Own_Client.ViewModels
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public Product _Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPrice _ProductPrice { get; set; }
        public int Count { get; set; }
    }
}
