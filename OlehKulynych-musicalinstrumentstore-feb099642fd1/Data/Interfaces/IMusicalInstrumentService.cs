using Microsoft.AspNetCore.Mvc.Rendering;
using Musical_Instrument_Store.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface IMusicalInstrumentService
    {
        Task<MusicalInstrumentListViewModel> GetMusicalInstrumentAsync();
        Task<MusicalInstrumentViewModel> GetMIbyIdAsync(int miId);
        Task AddMusicalInstrumentAsync(MusicalInstrumentViewModel musicalInstrumentViewModel, IFormFile uploadedImage);
        Task<MusicalInstrumentViewModel> GetMusicalInstrumentViewModelAsync();
        Task<MusicalInstrumentViewModel> GetMusicalInstrumentViewModelAsync(int id);
        Task DeleteMusicalInstrumentAsync(int id);
        Task EditAsync(MusicalInstrumentViewModel model, IFormFile uploadedImage);
        Task<MusicalInstrumentListViewModel> SearchMusicalInstrumentByStringAsync(string searchString);
        Task<MusicalInstrumentListViewModel> FilterAndSearchByCategoryAsync(int? categoryId, string searchString);
    }
}
