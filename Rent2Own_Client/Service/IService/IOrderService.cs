using Rent2Own_Models;

namespace Rent2Own_Client.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAll(string? userId);
        public Task<Order> Get(int orderId);

        public Task<Order> Create(StripePayment payment);

        public Task<OrderHeader> MarkPaymentSuccessful(OrderHeader orderHeader);
    }
}
