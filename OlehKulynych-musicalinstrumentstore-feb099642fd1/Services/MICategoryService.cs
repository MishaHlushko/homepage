using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Services
{
    public class MICategoryService : IMICategoryService
    {
        private readonly IMICategoryRepository _mICategoryRepository;

        public MICategoryService(IMICategoryRepository mICategoryRepository)
        {
            _mICategoryRepository = mICategoryRepository;
        }
        public async Task<IEnumerable<MICategory>> GetMICategories()
        {
            return await _mICategoryRepository.GetMICategories();
        }
        public async Task Create(string name)
        {
            await _mICategoryRepository.Create(name);
        }

        public async Task<MICategoryViewModel> GetMICategoryByIdAsync(int id)
        {
            MICategory mICategory = await _mICategoryRepository.GetMICategoryByIdAsync(id);

            if(mICategory != null)
            {
                return new MICategoryViewModel(mICategory);
            }
            return null;
        }

        public async Task UpdateCategoryAsync(int Id, string nameMI)
        {
            await _mICategoryRepository.UpdateCategoryAsync(Id, nameMI);
        }

        public async Task DeleteCategory(int id)
        {
            await _mICategoryRepository.DeleteCategory(id);
        }
    }
}
