using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Models
{
    public class Order
    {
        public OrderHeader OrderHeader {  get; set; }
        public List<OrderDetail> OrderDetails { get; set; }  
    }
}
