using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Musical_Instrument_Store.Data.Interfaces;
using Musical_Instrument_Store.Data.Models;
using Musical_Instrument_Store.ViewModels;

namespace Musical_Instrument_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly IMusicalInstrumentService _musicalInstrumentService;
        private readonly ICartService _cartService;
        private Cart _cart;
        public CartController(IMusicalInstrumentService musicalInstrumentService, ICartService cartService, Cart cart)
        {
            _musicalInstrumentService = musicalInstrumentService;
            _cartService = cartService;
            _cart = cart;
        }

        public async Task<IActionResult> Index()
        {
            
            var elements = await _cartService.GetCartLines(_cart);
            _cart.cartLines = elements;

            var obj = new CartViewModel { cartViewModel = _cart };

            return View(obj);
        }

        public async Task<RedirectToActionResult> AddToCart(int id)
        {
            var item = await _musicalInstrumentService.GetMIbyIdAsync(id);
            if (item != null)
            {
                TempData["AlertMessage"] = "Успішно додано!";
                await _cartService.AddToCartAsync(item, _cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCartLine(int cartlineId)
        {
            try
            {
                TempData["AlertMessage"] = "Успішно видалено!";
                await _cartService.DeleteCartLineAsync(cartlineId);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
