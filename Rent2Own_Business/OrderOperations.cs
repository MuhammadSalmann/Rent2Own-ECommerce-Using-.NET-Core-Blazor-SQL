using Microsoft.EntityFrameworkCore;
using Rent2Own_Business.Interfaces;
using Rent2Own_Common;
using Rent2Own_DataAccess;
using Rent2Own_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Business
{
    public class OrderOperations : IOrderOperations
    {
        private readonly DBHelper _db;

        public OrderOperations(DBHelper db)
        {
            _db = db;
        }

        public async Task<OrderHeader> CancelOrder(int id)
        {
            var orderHeader = await _db.OrderHeaders.FindAsync(id);
            orderHeader.ShippingDate = DateTime.Now;
            if (orderHeader == null)
            {
                return new OrderHeader();
            }

            if (orderHeader.Status == SD.Status_Pending)
            {
                orderHeader.Status = SD.Status_Cancelled;
                await _db.SaveChangesAsync();
            }
            if (orderHeader.Status == SD.Status_Confirmed)
            {
                //refund
                var options = new Stripe.RefundCreateOptions
                {
                    Reason = Stripe.RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };

                var service = new Stripe.RefundService();
                Stripe.Refund refund = service.Create(options);

                orderHeader.Status = SD.Status_Refunded;
                await _db.SaveChangesAsync();
            }
            return orderHeader;
        }

        public async Task<Order> Create(Order obj)
        {
            try
            {
                _db.OrderHeaders.Add(obj.OrderHeader);
                await _db.SaveChangesAsync();

                foreach (var details in obj.OrderDetails)
                {
                    details.OrderHeaderId = obj.OrderHeader.Id;
                }
                _db.OrderDetails.AddRange(obj.OrderDetails);
                await _db.SaveChangesAsync();

                return new Order()
                {
                    OrderHeader = obj.OrderHeader,
                    OrderDetails = (obj.OrderDetails).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return obj;
        }

        public async Task<int> Delete(int id)
        {
            var objHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if (objHeader != null)
            {
                List<OrderDetail> objDetail = _db.OrderDetails.Where(u => u.OrderHeaderId == id).ToList();

                _db.OrderDetails.RemoveRange(objDetail);
                _db.OrderHeaders.Remove(objHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<Order> Get(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id).ToList(),
            };
            if (order != null)
            {
                return order;
            }
            return new Order();
        }

        public async Task<List<Order>> GetAll(string? userId = null, string? status = null)
        {

            List<Order> OrderFromDb = new List<Order>();
            List<OrderHeader> orderHeaderList = _db.OrderHeaders.ToList();
            List<OrderDetail> orderDetailList = _db.OrderDetails.ToList();

            foreach (OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = orderDetailList.Where(u => u.OrderHeaderId == header.Id).ToList(),
                };
                OrderFromDb.Add(order);
            }
            //do some filtering #TODO

            return OrderFromDb;

        }

        public async Task<OrderHeader> MarkPaymentSuccessful(int id, string PI)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return new OrderHeader();
            }
            if (data.Status == SD.Status_Pending)
            {
                data.PaymentIntentId = PI;
                data.Status = SD.Status_Confirmed;
                await _db.SaveChangesAsync();
                return data;
            }
            return new OrderHeader();
        }

        public async Task<OrderHeader> UpdateHeader(OrderHeader obj)
        {
            if (obj != null)
            {
                var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == obj.Id);
                orderHeaderFromDb.Name = obj.Name;
                orderHeaderFromDb.PhoneNumber = obj.PhoneNumber;
                orderHeaderFromDb.Carrier = obj.Carrier;
                orderHeaderFromDb.Tracking = obj.Tracking;
                orderHeaderFromDb.StreetAddress = obj.StreetAddress;
                orderHeaderFromDb.City = obj.City;
                orderHeaderFromDb.State = obj.State;
                orderHeaderFromDb.PostalCode = obj.PostalCode;
                orderHeaderFromDb.Status = obj.Status;

                await _db.SaveChangesAsync();
                return orderHeaderFromDb;
            }
            return new OrderHeader();
        }

        public async Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(orderId);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if (status == SD.Status_Shipped)
            {
                data.ShippingDate = DateTime.Now;
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}

