using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string nameClient { get; set; }
        public string surnameClient { get; set; }
        public string addressClient { get; set; }
        public string phoneClient { get; set; }
        public string emailClient { get; set; }
        public DateTime orderTime { get; set; }
        public int statusOrderId { set; get; }

        public Order order { get; set; }
        public IEnumerable<OrderDetail> orderDetails {get;set;}
        public IEnumerable<SelectListItem> statusOrders { get; set; }
    }
}
