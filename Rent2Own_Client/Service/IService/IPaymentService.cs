using Rent2Own_Models;

namespace Rent2Own_Client.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModel> Checkout(StripePayment model);

    }
}
