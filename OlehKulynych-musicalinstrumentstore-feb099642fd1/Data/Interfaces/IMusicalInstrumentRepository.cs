using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface IMusicalInstrumentRepository
    {
        Task<IEnumerable<MusicalInstrument>> GetMusicalInstrument();
        Task<MusicalInstrument> GetMIbyIdAsync(int miId);
        Task<IEnumerable<SelectListItem>> GetCategoriesForDropdownAsync();
        Task AddMusicalInstrumentAsync(MusicalInstrument musicalInstrument);
        Task DeleteMusicalInstrumentAsync(MusicalInstrument musicalInstrument);
        Task EditAsync(MusicalInstrument musicalInstrument);
        Task<List<MusicalInstrument>> SearchMusicalInstrumentByStringAsync(string searchString);
        Task<List<MusicalInstrument>> FilterAndSearchByCategoryAsync(int? categoryId, string searchString);
        Task<IEnumerable<SelectListItem>> GetCategoriesForDropdownForFilterAsync();
    }
}
