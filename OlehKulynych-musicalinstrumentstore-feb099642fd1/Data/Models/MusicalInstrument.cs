using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Models
{
    public class MusicalInstrument
    {
        public int Id { set; get; }
        public string nameMI { set; get; }
        public string descMI { set; get; }
        public ushort price { set; get; }
        public string img { set; get; }
        public bool available { set; get; }
        public bool IsDeleted { get; set; }
        public int quantity { get; set; }
        public int MICategoryId { set; get; }
        public virtual MICategory MICategory { set; get; }

    }
}
