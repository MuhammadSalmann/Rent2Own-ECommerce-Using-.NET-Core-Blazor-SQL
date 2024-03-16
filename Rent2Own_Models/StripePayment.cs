using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent2Own_Models
{
    public class StripePayment
    {
        public StripePayment()
        {
            SuccessUrl = "shop/true";
            CancelUrl = "summary";
        }
        public Order _Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
