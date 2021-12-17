using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_Instrument_Store.Data.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task AddToCartAsync(MusicalInstrumentViewModel musicalInstrument, Cart cart)
        {
            await _cartRepository.AddToCartAsync(musicalInstrument, cart);
        }

        public Task DeleteCartLineAsync(int id)
        {
            return _cartRepository.DeleteCartLineAsync(id);
        }

        public async Task<List<CartLine>> GetCartLines(Cart cart)
        {
           return await Task.Run(() => _cartRepository.GetCartLines(cart));
        }
    }
}
