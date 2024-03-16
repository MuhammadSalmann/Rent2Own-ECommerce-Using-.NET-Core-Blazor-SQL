using Newtonsoft.Json;
using Rent2Own_Client.Service.IService;
using Rent2Own_Models;
using System.Text;

namespace Rent2Own_Client.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessModel> Checkout(StripePayment model)
        {
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);  
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessModel>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorModel>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


       
    }
}
