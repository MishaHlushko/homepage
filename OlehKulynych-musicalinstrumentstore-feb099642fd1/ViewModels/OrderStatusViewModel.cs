using Musical_Instrument_Store.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.ViewModels
{
    public class OrderStatusViewModel
    {
        public int Id { get; set; }
        public string statusOrderName { get; set; }
        public Order order { get; set; }

    }
}
