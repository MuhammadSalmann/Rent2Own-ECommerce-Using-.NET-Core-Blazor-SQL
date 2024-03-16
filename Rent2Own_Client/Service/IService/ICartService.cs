using Rent2Own_Client.ViewModels;

namespace Rent2Own_Client.Service.IService
{
    public interface ICartService
    {
		public event Action OnChange;
		Task DecrementCart(ShoppingCart shoppingCart);
        Task IncrementCart(ShoppingCart shoppingCart);
    }
}
