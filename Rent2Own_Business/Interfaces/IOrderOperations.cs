using Rent2Own_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Business.Interfaces
{
    public interface IOrderOperations
    {
        public Task<Order> Get(int id);
        public Task<List<Order>> GetAll(string? userId = null, string? status = null);
        public Task<Order> Create(Order obj);
        public Task<int> Delete(int id);

        public Task<OrderHeader> UpdateHeader(OrderHeader obj);

        public Task<OrderHeader> MarkPaymentSuccessful(int id, string PaytentID);
        public Task<bool> UpdateOrderStatus(int orderId, string status);

        public Task<OrderHeader> CancelOrder(int id);
    }
}
