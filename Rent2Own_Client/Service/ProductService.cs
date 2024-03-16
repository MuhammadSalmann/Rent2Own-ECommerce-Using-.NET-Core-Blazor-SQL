 using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Rent2Own_Client.Service.IService;
using Rent2Own_Models;

namespace Rent2Own_Client.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
        public async Task<Product> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<Product>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<List<Product>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<Product>>(content);
                foreach (var prod in products)
                {
                    prod.ImageUrl = BaseServerUrl + prod.ImageUrl;
                }
                return products;
            }
            return new List<Product>();
        }
    }
}
