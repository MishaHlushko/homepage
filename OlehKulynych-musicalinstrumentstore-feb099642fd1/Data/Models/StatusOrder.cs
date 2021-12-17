using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Models
{
    public class StatusOrder
    {
        public int Id { get; set; }
        public string statusName { get; set; }

        public List<Order> orders { set; get; }
    }
}
