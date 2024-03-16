using Rent2Own_Models;

namespace Rent2Own_Client.Service.IService
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> Get(int productId);
    }
}
