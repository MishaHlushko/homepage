using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int orderId { get; set; }
        public int musicalInstrumentId { get; set; }
        public uint price { get; set; }
        public virtual MusicalInstrument musicalInstrument { get; set; }
        public virtual Order order { get; set; }
    }
}
