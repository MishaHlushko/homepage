using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Musical_Instrument_Store.Data.Repository
{
    public class MusicalInstrumentRepository : IMusicalInstrumentRepository
    {
        private readonly AppDBContext _appDBContext;
        public MusicalInstrumentRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task<IEnumerable<MusicalInstrument>> GetMusicalInstrument()
        {
            return await _appDBContext.MusicalInstruments.ToListAsync();
        }

        public async Task<MusicalInstrument> GetMIbyIdAsync(int miId)
        {
            return await _appDBContext.MusicalInstruments.FirstOrDefaultAsync(MusicalInstruments => MusicalInstruments.Id == miId);
        }

        public async Task AddMusicalInstrumentAsync(MusicalInstrument musicalInstrument)
        {
            await _appDBContext.MusicalInstruments.AddAsync(musicalInstrument);
            _appDBContext.SaveChanges();
        }

        public async Task DeleteMusicalInstrumentAsync(MusicalInstrument musicalInstrument)
        {
            _appDBContext.MusicalInstruments.FirstOrDefault(p => p.Id == musicalInstrument.Id).IsDeleted = true;
            await _appDBContext.SaveChangesAsync();

        }

        public async Task EditAsync(MusicalInstrument musicalInstrument)
        {
            _appDBContext.ChangeTracker.Clear();

            _appDBContext.MusicalInstruments.Update(musicalInstrument);
            await _appDBContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<SelectListItem>> GetCategoriesForDropdownAsync()
        {
            List<SelectListItem> categories = await _appDBContext.mICategories.Select(n =>
            new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.categoryName
            }).ToListAsync();

            return new SelectList(categories, "Value", "Text");
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesForDropdownForFilterAsync()
        {
            List<SelectListItem> categories = await _appDBContext.mICategories.Select(n =>
            new SelectListItem
            {
                Value = n.Id.ToString(),
                Text = n.categoryName
            }).ToListAsync();

            SelectListItem selectListItem = new SelectListItem { Value = "0", Text = "All" };
            categories.Add(selectListItem);


            return new SelectList(categories, "Value", "Text");
        }
        public async Task<List<MusicalInstrument>> SearchMusicalInstrumentByStringAsync(string searchString)
        {
            return await _appDBContext.MusicalInstruments.Where(m => m.nameMI.Contains(searchString)).ToListAsync();
        }

        public async Task<List<MusicalInstrument>> FilterAndSearchByCategoryAsync(int? categoryId, string searchString)
        {
            IQueryable<MusicalInstrument> musicalInstruments = _appDBContext.MusicalInstruments.Include(m => m.MICategory).Where(m => m.MICategoryId == categoryId);

            if (!string.IsNullOrEmpty(searchString))
            {
                musicalInstruments = musicalInstruments.Where(m => m.nameMI.Contains(searchString));
            }


            return await musicalInstruments.ToListAsync();

        }
    }
}
