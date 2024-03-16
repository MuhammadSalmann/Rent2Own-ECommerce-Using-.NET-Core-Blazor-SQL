using Newtonsoft.Json;
using Rent2Own_Client.Service.IService;
using Rent2Own_Models;
using System.Text;

namespace Rent2Own_Client.Service
{
    public class OrderService : IOrderService
    {
		public readonly HttpClient _httpClient;
		public IConfiguration _configuration;
        public string BaseServerUrl;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<Order> Create(StripePayment payment)
        {
            var content = JsonConvert.SerializeObject(payment);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<Order>(responseResult);
                return result;
            }
            return new Order();

        }

        public async Task<Order> Get(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<Order>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<Order>> GetAll(string? userId = null)
        {
            var response = await _httpClient.GetAsync("/api/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);

                return orders;
            }

            return new List<Order>();
        }

        public async Task<OrderHeader> MarkPaymentSuccessful(OrderHeader orderHeader)
        {
            var content = JsonConvert.SerializeObject(orderHeader);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/paymentsuccessful", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderHeader>(responseResult);
                return result;
            }
            var errorModel = JsonConvert.DeserializeObject<ErrorModel>(responseResult);
            throw new Exception(errorModel.ErrorMessage);
        }
    }
}
