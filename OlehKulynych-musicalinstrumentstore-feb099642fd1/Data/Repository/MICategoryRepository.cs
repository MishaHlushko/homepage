using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;


namespace Musical_Instrument_Store.Data.Repository
{

    public class MICategoryRepository : IMICategoryRepository
    {
        private readonly AppDBContext appDBContext;
        public MICategoryRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }
        public async Task<IEnumerable<MICategory>> GetMICategories()
        {
           return await appDBContext.mICategories.ToListAsync();
        }

        public async Task<MICategory> GetMICategoryByIdAsync(int id)
        {
            return await appDBContext.mICategories.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task Create(string name)
        {
            await appDBContext.mICategories.AddAsync(new MICategory { categoryName = name, isDeleted = false });
            appDBContext.SaveChanges();
        }

        public async Task UpdateCategoryAsync(int Id, string nameMI)
        {
            var category = await GetMICategoryByIdAsync(Id);
            category.categoryName = nameMI;
            appDBContext.SaveChanges();
        }

        public async Task DeleteCategory(int id)
        {
            MICategory mICategory = await GetMICategoryByIdAsync(id);
            IEnumerable<MusicalInstrument> musicalInstruments = await appDBContext.MusicalInstruments.Where(m => m.MICategoryId == id).ToListAsync();

            foreach(MusicalInstrument m in musicalInstruments)
            {
                m.IsDeleted = true;
            }

            mICategory.isDeleted = true;
            appDBContext.SaveChanges();
        }
    }

}
