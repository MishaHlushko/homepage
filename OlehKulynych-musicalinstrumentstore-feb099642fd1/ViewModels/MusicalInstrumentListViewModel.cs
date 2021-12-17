using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;
namespace Musical_Instrument_Store.ViewModels
{
    public class MusicalInstrumentListViewModel
    {
        public IEnumerable<MusicalInstrument> ListMusicalInstruments { get; set; }
        public int categoryId { set; get; }
        public SelectList categories { get; set; }
    }
}
