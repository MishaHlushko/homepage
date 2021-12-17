using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;
namespace Musical_Instrument_Store.ViewModels
{
    public class OrderDetailListViewModel
    {
        public IEnumerable<OrderDetail> orderDetailsList { get; set; }
        public int statusOrderId { set; get; }
        public IEnumerable<SelectListItem> statusOrders { get; set; }
    }
}
