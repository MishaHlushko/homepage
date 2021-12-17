using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface IMICategoryService
    {
        Task<IEnumerable<MICategory>> GetMICategories();
        Task Create(string name);
        Task<MICategoryViewModel> GetMICategoryByIdAsync(int id);
        Task UpdateCategoryAsync(int Id, string nameMI);
        Task DeleteCategory(int id);
    }
}
