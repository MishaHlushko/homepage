using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDBContext appDBContext;
        public CartRepository(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        public static Cart GetCartAsync(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDBContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);
            return new Cart { CartId = cartId };
        }

        public async Task AddToCartAsync(MusicalInstrumentViewModel musicalInstrumentViewModel, Cart cart)
        {
            MusicalInstrument musicalInstrument = appDBContext.MusicalInstruments.SingleOrDefault(MusicalInstruments => MusicalInstruments.Id == musicalInstrumentViewModel.Id);
            appDBContext.cartLines.Add(new CartLine
            {
                CartId = cart.CartId,
                
                musicalInstrument = musicalInstrument,
                price = musicalInstrument.price
            });

            await appDBContext.SaveChangesAsync();
        }

        public async Task<List<CartLine>> GetCartLines(Cart cart)
        {
            return await Task.Run( () => appDBContext.cartLines.Where(c => c.CartId == cart.CartId).Include(s => s.musicalInstrument).ToList());
        }

        public async Task DeleteCartLineAsync(int id)
        {
            var cartline = appDBContext.cartLines.SingleOrDefault(c => c.Id == id);
            appDBContext.cartLines.Remove(cartline);

            await appDBContext.SaveChangesAsync();
        }

    }
}
