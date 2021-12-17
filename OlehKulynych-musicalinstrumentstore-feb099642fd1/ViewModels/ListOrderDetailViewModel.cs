using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.ViewModels
{
    public class ListOrderDetailViewModel
    {
        public List<OrderDetailViewModel> orderDetailViewModels { get; set; }

        public int statusOrderId { set; get; }
        public IEnumerable<SelectListItem> statusOrders { get; set; }
    }
}
