using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Interfaces
{
    public interface ICartService
    {
        public Task AddToCartAsync(MusicalInstrumentViewModel musicalInstrument, Cart cart);
        public Task<List<CartLine>> GetCartLines(Cart cart);
        public Task DeleteCartLineAsync(int id);
    }
}
