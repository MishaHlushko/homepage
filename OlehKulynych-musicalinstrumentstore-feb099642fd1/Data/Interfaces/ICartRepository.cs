using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface ICartRepository
    {
        
        public Task AddToCartAsync(MusicalInstrumentViewModel musicalInstrumentViewModel, Cart cart);
        public Task<List<CartLine>> GetCartLines(Cart cart);
        public Task DeleteCartLineAsync(int id);
    }
}
